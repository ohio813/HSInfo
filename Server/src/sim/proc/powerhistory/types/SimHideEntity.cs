using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimHideEntity : SimPowerHistory {
        public SimHideEntity(BinaryReader r) : base(SimPowerType.HIDE_ENTITY) {
            EntityID = r.ReadInt32();
            Zone = (TAG_ZONE)r.ReadInt32();
        }
        public int EntityID { get; set; }
        public TAG_ZONE Zone { get; set; }
    }
}
