using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimTagChange : SimPowerHistory {
        public SimTagChange(BinaryReader r) : base(SimPowerType.TAG_CHANGE) {
            EntityID = r.ReadInt32();
            TagName = r.ReadInt32();
            TagValue = r.ReadInt32();
        }
        public int EntityID { get; private set; }
        public int TagName { get; private set; }
        public int TagValue { get; private set; }
    }
}
