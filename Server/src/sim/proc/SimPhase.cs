using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimPhase {
        public enum Type {
            PLAY,
            BATTLECRY,
            AFTER_PLAY,
            AFTER_SUMMON,
            TARGETING,
            SPELL_TEXT,
            AFTER_SPELL,
            END_OF_TURN,
            START_OF_TURN,
            DRAW_A_CARD,
            COMBAT_PREP,
            COMBAT,
            HERO_POWER,
            INSPIRE
        }
    }
}
