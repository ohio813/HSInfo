
/*
 * File: src/message/types/game/MsgChooseOption.cs
 * Notes:
 */

using System.IO;

namespace HSInfo {
    public partial class MsgChooseOption : Message {
        /* --- Constructors --- */
        public MsgChooseOption(int main, int target, int position, int sub) : base(ID) {
            Main = main;
            Target = target;
            Position = position;
            Sub = sub;
        }
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            Main = r.ReadInt32();
            Target = r.ReadInt32();
            Position = r.ReadInt32();
            Sub = r.ReadInt32();
        }
        public override void Serialize(BinaryWriter w) {
            w.Write(Main);
            w.Write(Target);
            w.Write(Position);
            w.Write(Sub);
        }
        /* --- Instance Properties --- */
        public int Main { get; set; }
        public int Target { get; set; }
        public int Position { get; set; }
        public int Sub { get; set; }
    }
}
