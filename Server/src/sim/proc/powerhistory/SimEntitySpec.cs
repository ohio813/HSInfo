using System.Collections.Generic;
using System.IO;

namespace HSInfo {
    public class SimEntitySpec {
        public SimEntitySpec(BinaryReader r) {
            ID = r.ReadInt32();
            CardID = r.ReadString();

            Tags = new List<Network.Entity.Tag>();
            int c = r.ReadInt32();
            for (int i = 0; i < c; ++i) {
                var tag = new Network.Entity.Tag();
                tag.Name = r.ReadInt32();
                tag.Value = r.ReadInt32();
                Tags.Add(tag);
            }
        }
        public int ID { get; set; }
        public string CardID { get; set; }
        public List<Network.Entity.Tag> Tags { get; set; }
    }
}
