
/*
 * File: IPCMP.cpp
 * Notes:
 */

#include "IPCMP.hpp"
#include <atomic>
#include <Windows.h>
#include <iostream>
#include <fstream>

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
	// NEW ...
	volatile unsigned m_nServerActive;
	volatile unsigned m_nClientActive;

	unsigned m_nBufferSize;
	unsigned m_nBufferClientOffset, m_nBufferServerOffset;
	volatile bool m_bActive;
	volatile pos_t m_nInPosClient, m_nOutPosClient;
	volatile pos_t m_nInPosServer, m_nOutPosServer;
};


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
	ipc->m_nServerActive = 1;
	ipc->m_nClientActive = 0;

	// locate buffer locations...
	ipc->m_nBufferClientOffset = sizeof(ipc_t);
	ipc->m_nBufferServerOffset = sizeof(ipc_t) + bufferSize;

	// flush write buffer and set active (notifying client its safe to read contents)
	_Atomic_thread_fence(std::memory_order_release);
	ipc->m_bActive = true;

	return true;
}

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

	ipc->m_nClientActive = 1;

	std::_Atomic_thread_fence(std::memory_order_acquire);

	return ipc->m_nBufferSize;
}

int IPCMReadMessageClient(void *vIPC, char *buffer, int length) {
	ipc_t *ipc = (ipc_t*)vIPC;

	pos_t in, out;
	in.m_nAll = ipc->m_nInPosClient.m_nAll;
	out.m_nAll = ipc->m_nOutPosServer.m_nAll;

	if (in.m_nAll == out.m_nAll)
		return 0;

	char *serverBuffer = (char*)ipc + ipc->m_nBufferServerOffset;
	char *msgLoc = serverBuffer + in.m_nIndex;
	
	int msgSize;
	if ((in.m_nIndex > ipc->m_nBufferSize - 4) || (msgSize = *(int*)msgLoc) == -1) {
		in.m_nIndex = 0;
		in.m_nEven = !in.m_nEven;
		msgLoc = serverBuffer;
		msgSize = *((int*)msgLoc);
	}

	int adjMsgSize = (msgSize + sizeof(int) - 1) & ~(sizeof(int) - 1);
	if (adjMsgSize > length) {
		printf("message size (%d) is larger than buffer size (%d)...", adjMsgSize, length);
		return -1;
	}

	memcpy_s(buffer, msgSize, msgLoc + 4, msgSize);
	std::_Atomic_thread_fence(std::memory_order_acquire);

	in.m_nIndex += adjMsgSize + 4;
	ipc->m_nInPosClient.m_nAll = in.m_nAll;

	return msgSize;
}

int IPCMReadMessageServer(void *vIPC, char *buffer, int length) {
	ipc_t *ipc = (ipc_t*)vIPC;

	pos_t in, out;
	in.m_nAll = ipc->m_nInPosServer.m_nAll;
	out.m_nAll = ipc->m_nOutPosClient.m_nAll;

	if (in.m_nAll == out.m_nAll)
		return 0;

	char *clientBuffer = (char*)ipc + ipc->m_nBufferClientOffset;
	char *msgLoc = clientBuffer + in.m_nIndex;

	int msgSize;
	if ((in.m_nIndex > ipc->m_nBufferSize - 4) || (msgSize = *(int*)msgLoc) == -1) {
		in.m_nIndex = 0;
		in.m_nEven = !in.m_nEven;
		msgLoc = clientBuffer;
		msgSize = *((int*)msgLoc);
	}

	int adjMsgSize = (msgSize + sizeof(int) - 1) & ~(sizeof(int) - 1);
	if (adjMsgSize > length) {
		printf("message size (%d) is larger than buffer size (%d)...", adjMsgSize, length);
		return -1;
	}

	memcpy_s(buffer, msgSize, msgLoc + 4, msgSize);
	std::_Atomic_thread_fence(std::memory_order_acquire);

	in.m_nIndex += adjMsgSize + 4;
	ipc->m_nInPosServer.m_nAll = in.m_nAll;

	return msgSize;
}

int IPCMWriteMessageClient(void *vIPC, char *buffer, int length) {
	ipc_t *ipc = (ipc_t*)vIPC;
	unsigned bufferSize = ipc->m_nBufferSize;

	int newLength = (length + sizeof(int) - 1) & ~(sizeof(int) - 1);

	if ((unsigned)newLength > bufferSize) {
		printf("s.write message size (%d) is larger than the buffer (%d)...", newLength, bufferSize);
		return -1;
	}
	
	char *clientBuffer = (char*)ipc + ipc->m_nBufferClientOffset;

	pos_t out, in;
	out.m_nAll = ipc->m_nOutPosClient.m_nAll;
	in.m_nAll = ipc->m_nInPosServer.m_nAll;

	unsigned next = out.m_nIndex + newLength;
	if (next > bufferSize) {
		next = newLength;
		if (in.m_nIndex < next) {
			return -1;
		}
		if (out.m_nIndex <= bufferSize - 4)
			*(int*)(clientBuffer + out.m_nIndex) = -1; // notify client to check wraparound...
		out.m_nIndex = 0;
		out.m_nEven = !out.m_nEven;
	} else if (in.m_nEven != out.m_nEven && in.m_nIndex >= out.m_nIndex && in.m_nIndex < next) {
		return -1;
	}

	memcpy_s(clientBuffer + out.m_nIndex, length, buffer, length);
	out.m_nIndex = next;

	std::_Atomic_thread_fence(std::memory_order_release);
	ipc->m_nOutPosClient.m_nAll = out.m_nAll;

	return length;
}

int IPCMWriteMessageServer(void *vIPC, char *buffer, int length) {
	ipc_t *ipc = (ipc_t*)vIPC;
	unsigned bufferSize = ipc->m_nBufferSize;

	int newLength = (length + sizeof(int) - 1) & ~(sizeof(int) - 1);
	if ((unsigned)newLength > bufferSize) {
		printf("s.write message size (%d) is larger than the buffer (%d)...", newLength, bufferSize);
		return -1;
	}

	char *serverBuffer = (char*)ipc + ipc->m_nBufferServerOffset;

	pos_t out, in;
	out.m_nAll = ipc->m_nOutPosServer.m_nAll;
	in.m_nAll = ipc->m_nInPosClient.m_nAll;

	unsigned next = out.m_nIndex + newLength;
	if (next > bufferSize) {
		next = newLength;
		if (in.m_nIndex < next)
			return -1;
		if (out.m_nIndex <= bufferSize - 4)
			*((int*)(serverBuffer + out.m_nIndex)) = -1; // notify client to check wraparound...
		out.m_nIndex = 0;
		out.m_nEven = !out.m_nEven;
	} else if (in.m_nEven != out.m_nEven && in.m_nIndex >= out.m_nIndex && in.m_nIndex < next) {
		return -1;
	}

	memcpy_s(serverBuffer + out.m_nIndex, length, buffer, length);
	out.m_nIndex = next;

	std::_Atomic_thread_fence(std::memory_order_release);
	ipc->m_nOutPosServer.m_nAll = out.m_nAll;

	return length;
}

bool IPCMReset(mapping_t &map) {
	if (map.m_hnd == NULL || map.m_ptr == NULL)
		return false;
	ipc_t *ipc = (ipc_t*)map.m_ptr;
	ipc->m_nInPosClient.m_nAll = 0;
	ipc->m_nInPosServer.m_nAll = 0;
	ipc->m_nOutPosClient.m_nAll = 0;
	ipc->m_nOutPosServer.m_nAll = 0;
	return true;
}

bool IPCMCloseServer(mapping_t &map) {
	if (map.m_hnd == NULL || map.m_ptr == NULL)
		return false;

	ipc_t* ipc = (ipc_t*)map.m_ptr;
	ipc->m_nServerActive = 0;

	UnmapViewOfFile(map.m_ptr);
	CloseHandle(map.m_hnd);
	map.m_ptr = nullptr;
	map.m_hnd = nullptr;

	return true;
}

bool IPCMCloseClient(mapping_t &map) {	
	if (map.m_hnd == NULL || map.m_ptr == NULL)
		return false;

	ipc_t* ipc = (ipc_t*)map.m_ptr;
	ipc->m_nClientActive = 0;

	UnmapViewOfFile(map.m_ptr);
	CloseHandle(map.m_hnd);
	map.m_ptr = nullptr;
	map.m_hnd = nullptr;
	
	return true;
}
