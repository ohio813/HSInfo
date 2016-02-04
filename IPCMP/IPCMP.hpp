
/*
 * File: IPCMP.hpp
 * Notes:
 */

#pragma once

#define IPCMP_LIB extern "C" __declspec(dllexport)

#include <Windows.h>

struct mapping_t {
	HANDLE m_hnd;
	void *m_ptr;
};

IPCMP_LIB bool IPCMCreateMap(mapping_t &map, const char *name, int bufferSize);
IPCMP_LIB int IPCMConnect(mapping_t &map, const char *name);

IPCMP_LIB int IPCMReadMessageClient(void *vIPC, char *buffer, int length);
IPCMP_LIB int IPCMReadMessageServer(void *vIPC, char *buffer, int length);

IPCMP_LIB int IPCMWriteMessageClient(void *vIPC, char *buffer, int length);
IPCMP_LIB int IPCMWriteMessageServer(void *vIPC, char *buffer, int length);

IPCMP_LIB bool IPCMCloseServer(mapping_t &map);
IPCMP_LIB bool IPCMCloseClient(mapping_t &map);

IPCMP_LIB bool IPCMReset(mapping_t &map);