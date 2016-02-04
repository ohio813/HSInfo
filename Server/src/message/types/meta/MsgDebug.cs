
/*
 * File: MsgDebug.cs
 * Notes:
 */

using System.IO;

namespace HSInfo {
    public partial class MsgDebug : Message {
        /* --- Constructors --- */
        public MsgDebug(string msg) : base(ID) { Message = msg; }
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            Message = r.ReadString();
        }
        public override void Serialize(BinaryWriter w) {
            w.Write(Message);
        }
        /* --- Properties --- */
        public string Message { get; set; }
    }
}
