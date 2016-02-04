
/*
 * File: ClientMap.cs
 * Notes:
 */

using System;
using System.Runtime.InteropServices;

namespace IPCM {
    public class ClientMap : Map {
        /* --- Constructors & Destructor --- */
        private ClientMap(string name) : base(name) { }
        ~ClientMap() {
            Close();
        }
        /* --- Instance Methods (Interface) --- */
        public bool Write(byte[] buffer, int length) {
            if (m_isOpen == false) {
                Console.WriteLine("Map is not open...");
                return false;
            }
            return IPCMWriteMessageClient(m_mapData.m_ptr, buffer, length) != -1;
        }
        public int Read(byte[] buffer) {
            return IPCMReadMessageClient(m_mapData.m_ptr, buffer, buffer.Length);
        }
        public void Close() {
            if (m_isOpen) {
                IPCMCloseClient(ref m_mapData);
                m_isOpen = false;
            }
        }
        /* --- Static Methods (Interface) --- */
        public static ClientMap Create(string name) {
            ClientMap map = new ClientMap(name);
            int size = IPCMConnect(ref map.m_mapData, name);
            if (size == -1)
                return null;
            map.m_bufferSize = size;
            return map;
        }
        /* --- DLL-Defs --- */
        [DllImport("IPCMP.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int IPCMWriteMessageClient(IntPtr vIPC, byte[] buffer, int length);
        [DllImport("IPCMP.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int IPCMReadMessageClient(IntPtr vIPC, byte[] buffer, int length);
        [DllImport("IPCMP.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool IPCMCloseClient(ref MapData map);
    }
}
