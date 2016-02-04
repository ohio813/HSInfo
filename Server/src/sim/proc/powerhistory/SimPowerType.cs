using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public enum SimPowerType {
        ACTION_END = 0x6,
        ACTION_START = 0x5,
        CREATE_GAME = 0x7,
        FULL_ENTITY = 0x1,
        HIDE_ENTITY = 0x3,
        META_DATA = 0x8,
        SHOW_ENTITY = 0x2,
        TAG_CHANGE = 0x4
    }
}
