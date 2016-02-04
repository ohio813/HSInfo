using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public enum SimBlockType : int {
        ATTACK = 0x1,
        DEATHS = 0x6,
        FATIGUE = 0x8,
        JOUST = 0x2,
        PLAY = 0x7,
        POWER = 0x3,
        TRIGGER = 0x5
    }
}
