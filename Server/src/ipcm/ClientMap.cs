
using System;
using System.Runtime.InteropServices;

namespace IPCM {
    public class ClientMap : Map {
        /* --- Constructors --- */
        private ClientMap(string name) : base(name) { }
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
        [DllImport("IPCRoot.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int IPCMWriteMessageClient(IntPtr vIPC, byte[] buffer, int length);
        [DllImport("IPCRoot.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int IPCMReadMessageClient(IntPtr vIPC, byte[] buffer, int length);
    }
}
