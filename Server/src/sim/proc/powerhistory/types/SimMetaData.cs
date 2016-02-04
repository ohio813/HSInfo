using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimMetaData : SimPowerHistory {
        public SimMetaData(BinaryReader r) : base(SimPowerType.META_DATA) {
            MetaType = r.ReadInt32();
            Data = r.ReadInt32();
            Info = new List<int>();
            int c = r.ReadInt32();
            for (int i = 0; i < c; ++i) {
                Info.Add(r.ReadInt32());
            }
        }
        public int MetaType { get; private set; }
        public int Data { get; private set; }
        public List<int> Info { get; private set; }
    }
}
