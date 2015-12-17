
using System.IO;

namespace HSInfo {
    namespace MessageTypes {
        public partial class GameState : Message {
            public GameState() : base(ID) { }
            public override void Deserialize(BinaryReader r) { }
            public override void Serialize(BinaryWriter w) { }
        }
    }
}
