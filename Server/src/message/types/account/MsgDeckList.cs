using System;
using System.Collections.Generic;
using System.IO;

namespace HSInfo {
    public partial class MsgDeckList : Message {
        /* --- Inner Types --- */
        public class Deck {
            public long ID { get; set; }
            public string Name { get; set; }
            public TAG_CLASS HeroClass { get; set; }
        }
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            var list = new List<Deck>();
            int c = r.ReadInt32();
            for (int i = 0; i < c; ++i) {
                var deck = new Deck();
                deck.ID = r.ReadInt64();
                deck.Name = r.ReadString();
                deck.HeroClass = (TAG_CLASS)r.ReadInt32();
                list.Add(deck);
            }
            Decks = list;
        }
        public override void Serialize(BinaryWriter w) {
            w.Write(Decks.Count);
            foreach (var d in Decks) {
                w.Write(d.ID);
                w.Write(d.Name);
                w.Write((int)d.HeroClass);
            }
        }
        /* --- Properties --- */
        public List<Deck> Decks { get; set; } 
    }
}
