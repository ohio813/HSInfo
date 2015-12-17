

using System.IO;

namespace HSInfo {
    namespace MessageTypes {
        public partial class SetMouseTo : Message {
            public SetMouseTo() : base(ID) { }
            public SetMouseTo(float x, float y) : base(ID) { m_x = x; m_y = y; }
            public override void Deserialize(BinaryReader r) {
                m_x = r.ReadSingle();
                m_y = r.ReadSingle();
            }
            public override void Serialize(BinaryWriter w) {
                w.Write(m_x);
                w.Write(m_y);
            }
            public float m_x, m_y;
        }
    }
}
