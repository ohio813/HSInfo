
/*
 * File: MsgOptions.cs
 * Notes:
 */

using System.IO;
using System.Collections.Generic;

namespace HSInfo {
    public partial class MsgOptions : Message {
        public MsgOptions(List<Network.Options.Option> options) : base(ID) {
            Options = options;
        }
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            Options = new List<Network.Options.Option>();
            int count = r.ReadInt32();
            for (int i = 0; i < count; ++i) {
                var o = DeserializeOption(r);
                Options.Add(o);
            }
        }
        public override void Serialize(BinaryWriter w) {
            w.Write(Options.Count);
            foreach (var o in Options) {
                SerializeOption(o, w);
            }
        }
        /* --- Instance Methods (Auxiliary) --- */
        private void SerializeSubOption(Network.Options.Option.SubOption so, BinaryWriter w) {
            w.Write(so.ID);
            if (so.Targets == null) {
                w.Write(0);
            } else {
                w.Write(so.Targets.Count);
                foreach (int targetID in so.Targets) {
                    w.Write(targetID);
                }
            }
        }
        private Network.Options.Option.SubOption DeserializeSubOption(BinaryReader r) {
            var so = new Network.Options.Option.SubOption();

            so.ID = r.ReadInt32();
            int targetCount = r.ReadInt32();

            if (targetCount == 0) {
                return so;
            } else {
                so.Targets = new List<int>();
                for (int i = 0; i < targetCount; ++i) {
                    int id = r.ReadInt32();
                    so.Targets.Add(id);
                }
            }

            return so;
        }
        private void SerializeOption(Network.Options.Option o, BinaryWriter w) {
            w.Write((int)o.Type);

            SerializeSubOption(o.Main, w);

            var subs = o.Subs;
            if (subs == null) {
                w.Write(0);
            } else {
                w.Write(subs.Count);
                foreach (var so in subs) {
                    SerializeSubOption(so, w);
                }
            }
        }
        private Network.Options.Option DeserializeOption(BinaryReader r) {
            var o = new Network.Options.Option();

            o.Type = (Network.Options.Option.OptionType)r.ReadInt32();
            o.Main = DeserializeSubOption(r);

            int subCount = r.ReadInt32();
            o.Subs = new List<Network.Options.Option.SubOption>();
            for (int i = 0; i < subCount; ++i) {
                o.Subs.Add(DeserializeSubOption(r));
            }

            return o;
        }
        /* --- Properties --- */
        /* --- Instance Fields --- */
        public List<Network.Options.Option> Options { get; set; }
    }
}
