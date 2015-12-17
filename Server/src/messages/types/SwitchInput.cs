
using System.IO;

namespace HSInfo {
    namespace MessageTypes {
        public partial class SwitchInput : Message {
            public SwitchInput() : base(ID) { }
            public override void Deserialize(BinaryReader r) { }
            public override void Serialize(BinaryWriter w) { }
        }
    }
}
