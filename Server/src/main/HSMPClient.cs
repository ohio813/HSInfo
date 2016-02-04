
/*
 * File: HSMPClient.cs
 * Notes:
 */

using HSInfo;

namespace HSClient {
    public class HSMPClient {
        /* --- Constructors --- */
        private HSMPClient(ClientMessageStream s) {
            m_stream = s;
        }
        /* --- Instance Methods (Interface) --- */
        public bool SendDebugMessage(object o) {
            return m_stream.Write(new MsgDebug(o.ToString()));
        }
        public bool Send(Message m) {
            return m_stream.Write(m);
        }
        public void RegisterListener(int id, IMessageListener l) {
            m_stream.RegisterListener(id, l);
        }
        public ClientMessageStream GetStream() { return m_stream; }
        /* --- Static Methods (Interface) --- */
        public static bool Connect(string name) {
            if (s_instance != null) {
                s_instance.SendDebugMessage("ERROR: tried to connect with an already open HSMPClient...");
                return false;
            }
            ClientMessageStream s = ClientMessageStream.Create(name);
            if (s == null)
                return false;
            s_instance = new HSMPClient(s);
            return true;
        }
        public static HSMPClient Get() { return s_instance; }
        /* --- Instance Fields --- */
        private ClientMessageStream m_stream;
        /* --- Static Fields --- */
        private static HSMPClient s_instance;
    }
}
