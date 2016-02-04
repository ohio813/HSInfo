
using HSInfo;

namespace HSClient {
    namespace Account {
        public static class GoldNetSniffer {
            /* --- Static Methods (Interface) --- */
            public static void OnGoldBalance() {
                var pkt = ConnectAPI.GetGoldBalance();
                long balance = pkt.GetTotal();
                HSMPClient.Get().SendDebugMessage("OnGoldBalance: " + balance);
                HSMPClient.Get().Send(new MsgCurrencyBalance(MsgCurrencyBalance.CurrencyType.GOLD, balance));
            }
            public static void OnPurchaseWithGoldResponse() {
                var pkt = ConnectAPI.GetPurchaseWithGoldResponse();
                long balance = NetCache.Get().GetNetObject<NetCache.NetCacheArcaneDustBalance>().Balance;
                long used = pkt.GoldUsed;
                HSMPClient.Get().SendDebugMessage("OnPurchaseWithGoldResponse: " + balance + " - " + used);
                HSMPClient.Get().Send(new MsgCurrencyBalance(MsgCurrencyBalance.CurrencyType.GOLD, balance - used));
            }
        }
    }
}
