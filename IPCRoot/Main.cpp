
#include <atomic>
#include <Windows.h>

struct pos_t {
	union {
		struct {
			unsigned m_nIndex : 31;
			unsigned m_nEven : 1;
		};
		unsigned m_nAll;
	};
};

struct ipc_t {
	unsigned m_nBufferSize;
	unsigned m_nBufferClientOffset, m_nBufferServerOffset;
	volatile bool m_bActive;
	volatile pos_t m_nInPosClient, m_nOutPosClient;
	volatile pos_t m_nInPosServer, m_nOutPosServer;
};

struct mapping_t {
	HANDLE m_hnd;
	void *m_ptr;
};

extern "C" __declspec(dllexport)
bool IPCMCreateMap(mapping_t &map, const char *name, int bufferSize) {
	int totalSize = sizeof(ipc_t) + 2 * bufferSize;
	
	map.m_hnd = CreateFileMapping(
		INVALID_HANDLE_VALUE,
		NULL,                    
		PAGE_READWRITE,          
		0,                       
		totalSize,               
		name);                

	if (map.m_hnd == NULL)
		return false;
	
	map.m_ptr = MapViewOfFile(map.m_hnd,
		FILE_MAP_ALL_ACCESS,
		0,
		0,
		0);

	if (map.m_ptr == NULL) {
		CloseHandle(map.m_hnd);
		return false;
	}

	// initialize IPC variables
	ipc_t *ipc = (ipc_t*)map.m_ptr;
	ipc->m_nBufferSize = bufferSize;
	ipc->m_nInPosClient.m_nAll = 0;
	ipc->m_nOutPosClient.m_nAll = 0;
	ipc->m_nInPosServer.m_nAll = 0;
	ipc->m_nOutPosServer.m_nAll = 0;

	// locate buffer locations...
	ipc->m_nBufferClientOffset = sizeof(ipc_t);
	ipc->m_nBufferServerOffset = sizeof(ipc_t) + bufferSize;

	// flush write buffer and set active (notifying client its safe to read contents)
	_Atomic_thread_fence(std::memory_order_release);
	ipc->m_bActive = true;

	return true;
}

extern "C" __declspec(dllexport)
int IPCMConnect(mapping_t &map, const char *name) {

	map.m_hnd = OpenFileMapping(FILE_MAP_ALL_ACCESS, false, name);
	if (map.m_hnd == NULL) {
		printf("mapping not found...\n");
		return -1;
	}
	
	map.m_ptr = (LPTSTR)MapViewOfFile(map.m_hnd,
		FILE_MAP_ALL_ACCESS,
		0,
		0,
		0);

	if (map.m_ptr == NULL) {
		CloseHandle(map.m_hnd);
		printf("MapViewOfFile failed...\n");
		return -1;
	}

	ipc_t *ipc = (ipc_t*)map.m_ptr;
	
	int k = 0;
	while (ipc->m_bActive == false && ++k < 1000);
	if (ipc->m_bActive == false) {
		printf("IPCM region never became active...\n");
		return -1;
	}

	std::_Atomic_thread_fence(std::memory_order_acquire);

	return ipc->m_nBufferSize;
}

extern "C" __declspec(dllexport)
int IPCMReadMessageClient(void *vIPC, char *buffer, int length) {
	ipc_t *ipc = (ipc_t*)vIPC;
	
	pos_t in, out;
	in.m_nAll = ipc->m_nInPosClient.m_nAll;
	out.m_nAll = ipc->m_nOutPosServer.m_nAll;

	if (in.m_nAll == out.m_nAll)
		return 0;
	if (in.m_nEven != out.m_nEven && in.m_nIndex == 0 && out.m_nIndex)
		return 0;

	char *serverBuffer = (char*)ipc + ipc->m_nBufferServerOffset;
	
	char *msgLoc = serverBuffer + in.m_nIndex;

	int msgSize = *((int*)msgLoc);
	if (msgSize > length) {
		printf("message size (%d) is larger than buffer size (%d)...", msgSize, length);
		return -1;
	}

	if (msgSize == -1) {
		in.m_nIndex = 0;
		in.m_nEven = !in.m_nEven;
		msgLoc = serverBuffer;
		msgSize = *((int*)msgLoc);
	}
	memcpy_s(buffer, msgSize, msgLoc + 4, msgSize);
	std::_Atomic_thread_fence(std::memory_order_acquire);

	if (in.m_nIndex + msgSize + 4 == ipc->m_nBufferSize) {
		in.m_nIndex = 0;
		in.m_nEven = !in.m_nEven;
	} else {
		in.m_nIndex += msgSize + 4;
	}

	ipc->m_nInPosClient.m_nAll = in.m_nAll;

	return msgSize;
}

extern "C" __declspec(dllexport)
int IPCMReadMessageServer(void *vIPC, char *buffer, int length) {
	ipc_t *ipc = (ipc_t*)vIPC;

	pos_t in, out;
	in.m_nAll = ipc->m_nInPosServer.m_nAll;
	out.m_nAll = ipc->m_nOutPosClient.m_nAll;

	if (in.m_nAll == out.m_nAll)
		return 0;
	if (in.m_nEven != out.m_nEven && in.m_nIndex == 0 && out.m_nIndex)
		return 0;

	char *clientBuffer = (char*)ipc + ipc->m_nBufferClientOffset;
	char *msgLoc = clientBuffer + in.m_nIndex;

	int msgSize = *((int*)msgLoc);
	if (msgSize > length) {
		printf("message size (%d) is larger than buffer size (%d)...", msgSize, length);
		return -1;
	}

	if (msgSize == -1) {
		in.m_nIndex = 0;
		in.m_nEven = !in.m_nEven;
		msgLoc = clientBuffer;
		msgSize = *((int*)msgLoc);
	}
	memcpy_s(buffer, msgSize, msgLoc + 4, msgSize);
	std::_Atomic_thread_fence(std::memory_order_acquire);

	if (in.m_nIndex + msgSize + 4 == ipc->m_nBufferSize) {
		in.m_nIndex = 0;
		in.m_nEven = !in.m_nEven;
	} else {
		in.m_nIndex += msgSize + 4;
	}

	ipc->m_nInPosServer.m_nAll = in.m_nAll;

	return msgSize;
}

extern "C" __declspec(dllexport)
int IPCMWriteMessageClient(void *vIPC, char *buffer, int length) {
	ipc_t *ipc = (ipc_t*)vIPC;
	unsigned bufferSize = ipc->m_nBufferSize;

	// round up message size to nearest 8 byte boundary
	length = (length + sizeof(int) - 1) & ~(sizeof(int) - 1);

	if ((unsigned)length > bufferSize) {
		printf("s.write message size (%d) is larger than the buffer (%d)...", length, bufferSize);
		return -1;
	}

	char *clientBuffer = (char*)ipc + ipc->m_nBufferClientOffset;

	pos_t out, in;
	out.m_nAll = ipc->m_nOutPosClient.m_nAll;
	in.m_nAll = ipc->m_nInPosServer.m_nAll;

	unsigned next = out.m_nIndex + length;
	if (next > bufferSize) {
		next = length;
		if (in.m_nEven == out.m_nEven && in.m_nIndex >= 0 && in.m_nIndex < next)
			return -1;
		if (out.m_nIndex < bufferSize - 1)
			*((int*)(clientBuffer + out.m_nIndex)) = -1; // notify client to check wraparound...
		out.m_nIndex = 0;
		out.m_nEven = !out.m_nEven;
	} else {
		if (in.m_nEven == out.m_nEven && in.m_nIndex > out.m_nIndex && in.m_nIndex < next)
			return -1;
	}

	memcpy_s(clientBuffer + out.m_nIndex, length, buffer, length);
	out.m_nIndex = next;

	std::_Atomic_thread_fence(std::memory_order_release);
	ipc->m_nOutPosClient.m_nAll = out.m_nAll;

	return length;
}

extern "C" __declspec(dllexport)
int IPCMWriteMessageServer(void *vIPC, char *buffer, int length) {
	ipc_t *ipc = (ipc_t*)vIPC;
	unsigned bufferSize = ipc->m_nBufferSize;

	// round up message size to nearest 8 byte boundary
	length = (length + sizeof(int) - 1) & ~(sizeof(int) - 1);

	if ((unsigned)length > bufferSize) {
		printf("s.write message size (%d) is larger than the buffer (%d)...", length, bufferSize);
		return -1;
	}

	char *serverBuffer = (char*)ipc + ipc->m_nBufferServerOffset;

	pos_t out, in;
	out.m_nAll = ipc->m_nOutPosServer.m_nAll;
	in.m_nAll = ipc->m_nInPosClient.m_nAll;
	
	unsigned next = out.m_nIndex + length;
	if (next > bufferSize) {
		next = length;
		if (in.m_nEven == out.m_nEven && in.m_nIndex >= 0 && in.m_nIndex < next)
			return -1;
		if (out.m_nIndex < bufferSize - 1)
			*((int*)(serverBuffer + out.m_nIndex)) = -1; // notify client to check wraparound...
		out.m_nIndex = 0;
		out.m_nEven = !out.m_nEven;
	} else {
		if (in.m_nEven == out.m_nEven && in.m_nIndex > out.m_nIndex && in.m_nIndex < next)
			return -1;
	}

	memcpy_s(serverBuffer + out.m_nIndex, length, buffer, length);
	out.m_nIndex = next;

	std::_Atomic_thread_fence(std::memory_order_release);
	ipc->m_nOutPosServer.m_nAll = out.m_nAll;

	return length;
}

extern "C" __declspec(dllexport)
bool IPCMClose(mapping_t &map) {
	UnmapViewOfFile(map.m_ptr);
	CloseHandle(map.m_hnd);
	map.m_ptr = nullptr;
	map.m_hnd = nullptr;
	return true;
}

