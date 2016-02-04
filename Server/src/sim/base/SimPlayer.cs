using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimPlayer : SimEntity {
        /* --- Constructors --- */
        public SimPlayer() { }
        public SimPlayer(SimPlayerSpec spec) : base(spec.EntitySpec) {
            IsLocal = spec.IsLocal;
            Deck = new SimDeck();
            ZonePlay = new SimZone();
            ZoneHand = new SimZone();
            ZoneGraveyard = new SimZone();
        }
        /* --- Instance Properties --- */
        public SimEntity Hero { get; set; }
        public SimEntity HeroPower { get; set; }
        public SimZone ZonePlay { get; set; }
        public SimZone ZoneHand { get; set; }
        public SimZone ZoneGraveyard { get; set; }
        public SimDeck Deck { get; set; }
        public bool IsLocal { get; set; }
    }
}
