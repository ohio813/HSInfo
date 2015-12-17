using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    namespace MessageTypes {
        public partial class DebugMessage {
            public const int ID = 0;
        }
        public partial class SwitchInput {
            public const int ID = 1;
        }
        public partial class CurrentScene {
            public const int ID = 3;
        }
        public partial class SetMouseTo {
            public const int ID = 4;
        }
        public partial class HSPacket {
            public const int ID = 5;
        }
        public partial class GameState {
            public const int ID = 6;
        }
    }
}
