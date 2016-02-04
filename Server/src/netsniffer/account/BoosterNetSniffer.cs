
/*
 * File: netsniffer/account/BoosterNetSniffer.cs
 * Notes:
 */


using HSInfo;

namespace HSClient {
    namespace Account {
        public static class BoosterNetSniffer {
            /* --- Static Methods (Interface) --- */
            public static void OnBoosterList() {
                HSMPClient.Get().SendDebugMessage("OnBoosterList(" + ConnectAPI.GetBoosters().BoosterStacks.Count + ")");
                int count = 0;
                foreach (var bs in ConnectAPI.GetBoosters().BoosterStacks) {
                    HSMPClient.Get().SendDebugMessage(bs.Id + " : " + bs.Count);
                    count += bs.Count;
                }
                HSMPClient.Get().Send(new MsgCurrencyBalance(MsgCurrencyBalance.CurrencyType.PACKS, count));
            }
            public static void OnBoosterTally() {
                HSMPClient.Get().SendDebugMessage("OnBoosterTallyList(" + ConnectAPI.GetBoosterTallies().BoosterTallies.Count + ")");
                foreach (var bt in ConnectAPI.GetBoosterTallies().BoosterTallies) {
                    HSMPClient.Get().SendDebugMessage(bt.Id + " : " + bt.Count);
                }
            }
            public static void OnBoosterContent() {
                var cards = ConnectAPI.GetOpenedBooster();

                var msg = new MsgOpenedPack();
                msg.Cards = new System.Collections.Generic.List<NetCache.BoosterCard>();
                foreach (var c in cards) {
                    msg.Cards.Add(c);
                }

                HSMPClient.Get().Send(msg);
            }
        }
    }
}
