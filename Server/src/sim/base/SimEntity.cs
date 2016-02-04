using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimEntity {
        public SimEntity() {
            Tags = new TagSet();
            Enchantments = new List<SimEntity>();
        }
        public SimEntity(SimEntitySpec spec) {
            ID = spec.ID;
            CardID = spec.CardID;
            Tags = new TagSet();
            Tags.SetTags(spec.Tags);
            Enchantments = new List<SimEntity>();
        }
        public int ID { get; set; }
        public string CardID { get; set; }
        public TagSet Tags { get; set; }
        public List<SimEntity> Enchantments { get; set; }
    }
}
