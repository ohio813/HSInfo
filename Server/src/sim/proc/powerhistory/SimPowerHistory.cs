using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimPowerHistory {
        protected SimPowerHistory(SimPowerType type) {
            Type = type;
        }
        public SimPowerType Type { get; private set; }
    }
}
