
/*
 * File: MsgCurrencyBalance.cs
 * Notes:
 */

using System.IO;

namespace HSInfo {
    public partial class MsgCurrencyBalance : Message {
        /* --- Inner Types --- */
        public enum CurrencyType : int {
            GOLD,
            DUST,
            PACKS
        }
        /* --- Constructors --- */
        public MsgCurrencyBalance(CurrencyType type, long amount) : base(ID) {
            Type = type;
            Amount = amount;
        }
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            Type = (CurrencyType)r.ReadInt32();
            Amount = r.ReadInt64();
        }
        public override void Serialize(BinaryWriter w) {
            w.Write((int)Type);
            w.Write(Amount);
        }
        /* --- Properties --- */
        public long Amount { get; set; }
        public CurrencyType Type { get; set; }
    }
}
