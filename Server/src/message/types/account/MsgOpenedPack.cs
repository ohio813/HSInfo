
/*
 * File: MsgOpenedPack.cs
 * Notes:
 */

using System.Collections.Generic;
using System.IO;

namespace HSInfo {
    public partial class MsgOpenedPack : Message {
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            var count = r.ReadInt32();
            var list = new List<NetCache.BoosterCard>();
            for (int i = 0; i < count; ++i) {
                var bc = new NetCache.BoosterCard();
                bc.Date = r.ReadInt64();
                bc.Def = new NetCache.CardDefinition();
                bc.Def.Name = r.ReadString();
                bc.Def.Premium = (TAG_PREMIUM)r.ReadInt32();
                list.Add(bc);
            }
            Cards = list;
        }
        public override void Serialize(BinaryWriter w) {
            var list = Cards;
            w.Write(list.Count);
            foreach (var c in list) {
                w.Write(c.Date);
                w.Write(c.Def.Name);
                w.Write((int)c.Def.Premium);
            }
        }
        /* --- Properties --- */
        public List<NetCache.BoosterCard> Cards { get; set; }
    }
}
