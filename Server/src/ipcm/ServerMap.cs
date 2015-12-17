
using System;
using System.Runtime.InteropServices;

namespace IPCM {
    public class ServerMap : Map {
        /* --- Constructors --- */
        private ServerMap(string name) : base(name) { }
        /* --- Instance Methods (Interface) --- */
        public bool Write(byte[] buffer, int length) {
            if (m_isOpen == false) {
                Console.WriteLine("Map is not open...");
                return false;
            }
            return IPCMWriteMessageServer(m_mapData.m_ptr, buffer, length) != -1;
        }
        public int Read(byte[] buffer) {
            return IPCMReadMessageServer(m_mapData.m_ptr, buffer, buffer.Length);
        }
        /* --- Static Methods (Interface) --- */
        public static ServerMap Create(string name, int size) {
            ServerMap map = new ServerMap(name);
            if (IPCMCreateMap(ref map.m_mapData, name, size) == false) {
                Console.WriteLine("IPCMCreateMap failed...returning null");
                return null;
            }
            map.m_bufferSize = size;
            return map;
        }
        /* --- DLL-Defs --- */
        [DllImport("IPCRoot.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int IPCMWriteMessageServer(IntPtr vIPC, byte[] buffer, int length);
        [DllImport("IPCRoot.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int IPCMReadMessageServer(IntPtr vIPC, byte[] buffer, int length);
    }
}
