
/*
 * File: netsniffer/game/AllNetSniffer.cs
 * Notes:
 */

using HSInfo;

namespace HSClient {
    namespace Game {
        public class AllNetSniffer {
            /* --- Static Methods (Interface) --- */
            public static void OnEntitiesChosen() {
                HSMPClient.Get().SendDebugMessage("OnEntitiesChosen");
            }
            public static void OnEntityChoices() {
                var e = Network.GetEntityChoices();
                HSMPClient.Get().SendDebugMessage(e.Entities);
            }
            public static void OnTurnTimer() {
                HSMPClient.Get().SendDebugMessage("OnTurnTimer");
            }
            public static void OnPowerHistory() {
                var list = Network.GetPowerHistory();
                var phMsg = new MsgPowerHistory(list);
                HSMPClient.Get().Send(phMsg);
            }
        }
    }
}
