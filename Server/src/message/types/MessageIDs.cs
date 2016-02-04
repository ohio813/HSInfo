using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    /* --- Meta Messages --- */
    public partial class MsgDebug {
        public const int ID = 0x0;
        public MsgDebug() : base(ID) { }
    }
    public partial class MsgStatus {
        public const int ID = 0x3;
        public MsgStatus() : base(ID) { }
    }
    /* --- Game Messages --- */
    public partial class MsgChooseOption {
        public const int ID = 0x40;
        public MsgChooseOption() : base(ID) { }
    }
    public partial class MsgOptions {
        public const int ID = 0x43;
        public MsgOptions() : base(ID) { }
    }
    public partial class MsgPowerHistory {
        public const int ID = 0x45;
        public MsgPowerHistory() : base(ID) { }
    }
    /* --- Task Messages --- */
    public partial class MsgTaskSet {
        public const int ID = 0x20;
        public MsgTaskSet() : base(ID) { }
    }
    public partial class MsgTaskFinished {
        public const int ID = 0x21;
        public MsgTaskFinished() : base(ID) { }
    }
    public partial class MsgTaskGetState {
        public const int ID = 0x22;
        public MsgTaskGetState() : base(ID) { }
    }
    /* --- Account Messages --- */
    public partial class MsgCurrencyBalance {
        public const int ID = 0xA0;
        public MsgCurrencyBalance() : base(ID) { }
    }
    public partial class MsgAccountInfoResponse {
        public const int ID = 0xA1;
        public MsgAccountInfoResponse() : base(ID) { }
    }
    public partial class MsgAccountInfoRequest {
        public const int ID = 0xA2;
        public MsgAccountInfoRequest() : base(ID) { }
    }
    public partial class MsgOpenedPack {
        public const int ID = 0xA3;
        public MsgOpenedPack() : base(ID) { }
    }
    public partial class MsgDeckList {
        public const int ID = 0xA4;
        public MsgDeckList() : base(ID) { }
    }
}
