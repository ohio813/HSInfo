
/*
 * File: netsniffer/game/AllOptionsNetSniffer.cs
 * Notes:
 */

using HSInfo;

namespace HSClient {
    namespace Game {
        public class AllOptionsNetSniffer {
            /* --- Static Methods (Interface) --- */
            public static void OnAllOptions() {
                var pkt = Network.GetOptions();
                var msg = new MsgOptions(pkt.List);
                HSMPClient.Get().Send(msg);
            }
        }
    }
}
