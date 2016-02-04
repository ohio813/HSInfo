using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimShowEntity : SimPowerHistory {
        public SimShowEntity(BinaryReader r) : base(SimPowerType.SHOW_ENTITY) {
            EntitySpec = new SimEntitySpec(r);
        }
        public SimEntitySpec EntitySpec { get; set; }
    }
}
