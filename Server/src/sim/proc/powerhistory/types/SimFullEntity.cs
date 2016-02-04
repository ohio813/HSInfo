using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimFullEntity : SimPowerHistory {
        public SimFullEntity(BinaryReader r) : base(SimPowerType.FULL_ENTITY) {
            EntitySpec = new SimEntitySpec(r);
        }
        public SimEntitySpec EntitySpec { get; set; }
    }
}
