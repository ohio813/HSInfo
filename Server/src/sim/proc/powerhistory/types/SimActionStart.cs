using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimActionStart : SimPowerHistory {
        public SimActionStart(BinaryReader r) : base(SimPowerType.ACTION_START) {
            Index = r.ReadInt32();
            EntityID = r.ReadInt32();
            TargetID = r.ReadInt32();
            BlockType = (SimBlockType)r.ReadInt32();        
        }
        public int Index { get; private set; }
        public int EntityID { get; private set; }
        public int TargetID { get; private set; }
        public SimBlockType BlockType { get; private set; }
    }
}
