
using System.IO;

namespace HSInfo {
    public abstract class Message {
        /* --- Constructors --- */
        protected Message(int id) { m_id = id; }
        /* --- Instance Methods (Interface) --- */
        public int GetID() { return m_id; }
        public abstract void Serialize(BinaryWriter w);
        public abstract void Deserialize(BinaryReader r);
        /* --- Instance Fields --- */
        private int m_id;
    }
}
