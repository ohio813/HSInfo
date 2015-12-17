

using System;

namespace HSInfo {
    public class ClientMessageStream : MessageStream {
        /* --- Constructors --- */
        protected ClientMessageStream(IPCM.ClientMap map) : base(map.GetBufferSize()) {
            Console.WriteLine(map.GetBufferSize());
            Console.Out.Flush();
            m_map = map;
        }
        /* --- Instance Methods (Interface) --- */
        public bool Write(Message message) {
            m_outStream.BaseStream.Position = 0;
                
            m_outStream.Write(0);
            m_outStream.Write(message.GetID());
            message.Serialize(m_outStream);

            int size = (int)m_outStream.BaseStream.Position - 4;
            m_outStream.BaseStream.Position = 0;
            m_outStream.Write(size);

            return m_map.Write(m_outBuffer, size + 4);
        }
        public int CheckForMessages(MessageHandler h) {
            int messages = 0;

            int maxMessages = 16;
            while (true) {
                int msgLength = m_map.Read(m_inBuffer);
                if (msgLength > 0) {
                    m_inStream.BaseStream.Position = 0;
                    int id = m_inStream.ReadInt32();

                    Message msg = h(id);
                    if (msg != null) {
                        msg.Deserialize(m_inStream);
                        m_messages.Enqueue(msg);
                        if (++messages >= maxMessages) {
                            break;
                        }
                    } else {
                        Console.WriteLine("ID not recognized...");
                    }
                } else if (msgLength == -1) {
                    return -1;
                } else {
                    break;
                }
            }

            return messages;
        }
        public void Close() {
            m_map.Close();
        }
        /* --- Static Methods (Interface) --- */
        public static ClientMessageStream Create(string name) {
            IPCM.ClientMap map = IPCM.ClientMap.Create(name);
            if (map == null)
                return null;
            return new ClientMessageStream(map);
        }
        /* --- Instance Fields --- */
        private IPCM.ClientMap m_map;
    }

}
