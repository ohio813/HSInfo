using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimCreateGame : SimPowerHistory {
        public SimCreateGame(BinaryReader r) : base(SimPowerType.CREATE_GAME) {
            GameSpec = new SimEntitySpec(r);

            PlayerSpecs = new List<SimPlayerSpec>();
            int c = r.ReadInt32();
            for (int i = 0; i < c; ++i) {
                PlayerSpecs.Add(new SimPlayerSpec(r));
            }
        }
        public SimEntitySpec GameSpec { get; private set; }
        public List<SimPlayerSpec> PlayerSpecs { get; private set; }
    }
}
