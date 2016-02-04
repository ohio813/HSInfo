
/*
 * File: MsgAccountInfoResponse.cs
 * Notes:
 */

using System.IO;

namespace HSInfo {
    public partial class MsgAccountInfoResponse : Message {
        /* --- Constructors --- */
        public MsgAccountInfoResponse(string name, long gold, long dust, long packs, int rank, int stars) : base(ID) {
            Name = name;
            Gold = gold;
            Dust = dust;
            Packs = packs;
            Rank = rank;
            Stars = stars;
        }
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            Name = r.ReadString();
            Packs = r.ReadInt64();
            Gold = r.ReadInt64();
            Dust = r.ReadInt64();
            Rank = r.ReadInt32();
            Stars = r.ReadInt32();
        }
        public override void Serialize(BinaryWriter w) {
            if (Name == null) {
                w.Write("NULL");
            } else {
                w.Write(Name);
            }
            w.Write(Packs);
            w.Write(Gold);
            w.Write(Dust);
            w.Write(Rank);
            w.Write(Stars);
        }
        /* --- Properties --- */
        public string Name { get; set; }
        public long Gold { get; set; }
        public long Dust { get; set; }
        public long Packs { get; set; }
        public int Rank { get; set; }
        public int Stars { get; set; }
    }
}
