
/*
 * File: netsniffer/account/DustNetSniffer.cs
 * Notes:
 */

using HSInfo;

namespace HSClient {
    namespace Account {
        public static class DustNetSniffer {
            /* --- Static Methods (Interface) --- */
            public static void OnArcaneDustBalance() {
                long balance = ConnectAPI.GetArcaneDustBalance();
                HSMPClient.Get().SendDebugMessage("OnArcangeDustBalance: " + balance);
                HSMPClient.Get().Send(new MsgCurrencyBalance(MsgCurrencyBalance.CurrencyType.DUST, balance));
            }
            public static void OnBoughtSoldCard() {
                var pkt = ConnectAPI.GetCardSaleResult();
                long balance = NetCache.Get().GetNetObject<NetCache.NetCacheArcaneDustBalance>().Balance;
                HSMPClient.Get().SendDebugMessage("OnBoughtSoldCard: " + balance + " +- " + pkt.Amount);
                switch (pkt.Action) {
                    case Network.CardSaleResult.SaleResult.CARD_WAS_BOUGHT:
                        HSMPClient.Get().Send(new MsgCurrencyBalance(MsgCurrencyBalance.CurrencyType.DUST, balance - pkt.Amount));
                        break;
                    case Network.CardSaleResult.SaleResult.CARD_WAS_SOLD:
                        HSMPClient.Get().Send(new MsgCurrencyBalance(MsgCurrencyBalance.CurrencyType.DUST, balance + pkt.Amount));
                        break;
                }
            }
            public static void OnMassDisenchantResponse() {
                var pkt = ConnectAPI.GetMassDisenchantResponse();
                long balance = NetCache.Get().GetNetObject<NetCache.NetCacheArcaneDustBalance>().Balance;
                HSMPClient.Get().SendDebugMessage("OnMassDisenchantResponse: " + balance + " +- " + pkt.Amount);
                HSMPClient.Get().Send(new MsgCurrencyBalance(MsgCurrencyBalance.CurrencyType.DUST, balance + pkt.Amount));
            }
        }
    }
}
