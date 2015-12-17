
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
            m_listeners = new List<MessageListener>();
        }
        /* --- Inner Types --- */
        public delegate Message MessageHandler(int id);
        /* --- Instance Methods (Interface) --- */
        public void RegisterListener(MessageListener l) {
            m_listeners.Add(l);
        }
        public void UnregisterListener(MessageListener l) {
            m_listeners.Remove(l);
        }
        public void Distribute() {
            Message m;
            while (m_messages.Count > 0) {
                m = m_messages.Dequeue();
                foreach (MessageListener l in m_listeners) {
                    l.OnMessageRecv(m);
                }
            }
        }
        /* --- Instance Fields --- */
        protected byte[] m_inBuffer;
        protected byte[] m_outBuffer;
        protected BinaryReader m_inStream;
        protected BinaryWriter m_outStream;
        protected Queue<Message> m_messages;
        protected List<MessageListener> m_listeners;
        
    }
}
