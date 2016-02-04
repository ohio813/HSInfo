using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimPlayerSpec {
        public SimPlayerSpec(BinaryReader r) {
            EntitySpec = new SimEntitySpec(r);
            ID = r.ReadInt32();
            IsLocal = r.ReadBoolean();
        }
        public SimEntitySpec EntitySpec { get; private set; }
        public int ID { get; private set; }
        public bool IsLocal { get; private set; }
    }
}
