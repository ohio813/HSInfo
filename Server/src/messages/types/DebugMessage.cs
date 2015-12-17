
using System.IO;

namespace HSInfo {
    namespace MessageTypes {
        public partial class DebugMessage : Message {
            public DebugMessage() : base(ID) { }
            public DebugMessage(string msg) : base(ID) { m_msg = msg; }
            public override void Deserialize(BinaryReader r) {
                m_msg = r.ReadString();
            }
            public override void Serialize(BinaryWriter w) {
                w.Write(m_msg);
            }
            public string m_msg;
        }
    }
}
