
using System.Collections.Generic;
using System.IO;

namespace HSInfo {
    public class MessageStream {
        /* --- Constructors --- */
        protected MessageStream(int size) {
            m_inBuffer = new byte[size];
            m_outBuffer = new byte[size];
            m_inStream = new BinaryReader(new MemoryStream(m_inBuffer));
            m_outStream = new BinaryWriter(new MemoryStream(m_outBuffer));
            m_messages = new Queue<Message>();
            m_idListenerMap = new Map<int, List<IMessageListener>>();
        }
        /* --- Inner Types --- */
        public delegate Message MessageHandler(int id);
        /* --- Instance Methods (Interface) --- */
        public void RegisterListener(int id, IMessageListener l) {
            if (m_idListenerMap.ContainsKey(id) == false)
                m_idListenerMap[id] = new List<IMessageListener>();
            m_idListenerMap[id].Add(l);
        }
        public void Distribute() {
            Message m;
            while (m_messages.Count > 0) {
                m = m_messages.Dequeue();
                if (m_idListenerMap.ContainsKey(m.GetID()) == false)
                    continue;
                foreach (var l in m_idListenerMap[m.GetID()]) {
                    l(m);
                }
            }
        }
        /* --- Instance Fields --- */
        protected byte[] m_inBuffer;
        protected byte[] m_outBuffer;
        protected BinaryReader m_inStream;
        protected BinaryWriter m_outStream;
        protected Queue<Message> m_messages;
        protected Map<int, List<IMessageListener>> m_idListenerMap;
    }
}
