using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimZone {
        public int PlayerID { get; set; }
        public TAG_ZONE Type { get; set; }
        public List<Card> Cards { get; set; }
    }
}
