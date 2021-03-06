﻿
using System;
using System.Runtime.InteropServices;

namespace IPCM {
    public class Map {
        /* --- Constructors --- */
        protected Map(string name) {
            m_name = name;
            m_isOpen = true;
            m_mapData = new MapData();
            m_mapData.m_handle = (IntPtr)null;
            m_mapData.m_ptr = (IntPtr)null;
        }
        /* --- Instance Methods (Interface) --- */
        public void Debug() {
            Console.WriteLine("Map(" + this + "):");
            Console.WriteLine("\thandle: " + m_mapData.m_handle);
            Console.WriteLine("\tptr: " + m_mapData.m_ptr);
            Console.WriteLine("\tbuffer size: " + m_bufferSize);
        }
        public int GetBufferSize() { return m_bufferSize; }
        /* --- Instance Fields --- */
        protected MapData m_mapData;
        protected int m_bufferSize;
        protected string m_name;
        protected bool m_isOpen;
        /* --- DLL-Refs --- */
        [StructLayout(LayoutKind.Sequential)]
        protected struct MapData {
            public IntPtr m_handle;
            public IntPtr m_ptr;
        }
        [DllImport("IPCMP.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool IPCMCreateMap(ref MapData map, string name, int bufferSize);
        [DllImport("IPCMP.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int IPCMConnect(ref MapData map, string name);
    }

}
