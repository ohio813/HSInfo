using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSInfo {
    public class HSMPServer {
        /* --- Constructors --- */
        private HSMPServer(ServerMessageStream s) {
            m_server = s;
            m_threadListener = new Thread(DoListenLoop);
        }
        /* --- Instance Methods (Interface) --- */
        public void RegisterListener(int id, IMessageListener l) {
            m_server.RegisterListener(id, l);
        }
        public void RegisterUIListener(int id, Form form, IMessageListener l) {
            m_server.RegisterListener(id, delegate (Message msg) {
                form.Invoke(l, new object[] { msg });
            });
        }
        public void StartListenerThread() {
            m_doClose = false;
            m_threadListener.Start();
        }
        public void StopListenerThread() {
            m_doClose = true;
            if (m_threadListener.IsAlive) {
                m_threadListener.Join();
            }
        }
        public void Send(Message msg) {
            m_server.Write(msg);
        }
        public void SendDebugMessage(object obj) {
            m_server.Write(new MsgDebug(obj == null ? "null" : obj.ToString()));
        }
        public void Close() {
            m_server.Close();
        }
        /* --- Instance Methods (Auxiliary) --- */
        private void DoListenLoop() {
            while (m_doClose == false) {
                m_server.CheckForMessages(MessageHandler.Alloc);
                m_server.Distribute();

                System.Threading.Thread.Sleep(10);
            }
        }
        /* --- Static Methods (Interface) --- */
        public static HSMPServer Get() { return s_instance; }
        public static bool Create(string name, int size) {
            if (s_instance != null)
                return false;
            ServerMessageStream server = ServerMessageStream.Create(name, size);
            if (server == null)
                return false;
            s_instance = new HSMPServer(server);
            return true;
        }
        /* --- Instance Fields --- */
        private Thread m_threadListener;
        private ServerMessageStream m_server;
        private volatile bool m_doClose;
        /* --- Static Fields --- */
        private static HSMPServer s_instance;
    }
}
