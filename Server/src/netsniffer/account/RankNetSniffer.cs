
/*
 * File: netsniffer/account/RankNetSniffer.cs
 * Notes:
 */

using HSInfo;

namespace HSClient {
    namespace Account {
        public static class RankNetSniffer {
            /* --- Static Methods (Interface) --- */
            public static void OnMedalInfo() {
                var pkt = ConnectAPI.GetMedalInfo();
                var response = new MsgAccountInfoResponse();

                HSMPClient.Get().SendDebugMessage("OnMedalInfo {");
                HSMPClient.Get().SendDebugMessage("\tStarLevel: " + pkt.StarLevel);
                HSMPClient.Get().SendDebugMessage("\tStars: " + pkt.Stars);
                HSMPClient.Get().SendDebugMessage("\tStreak: " + pkt.Streak);
                HSMPClient.Get().SendDebugMessage("\tSeasonWins: " + pkt.SeasonWins);
                HSMPClient.Get().SendDebugMessage("\tStartStars: " + pkt.StartStars);
                HSMPClient.Get().SendDebugMessage("\tGainLevelStars: " + pkt.GainLevelStars);
                HSMPClient.Get().SendDebugMessage("\tCanLoseStars: " + pkt.CanLoseStars);
                HSMPClient.Get().SendDebugMessage("\tCanLoseLevel: " + pkt.CanLoseLevel);
                HSMPClient.Get().SendDebugMessage("\tBestStarLevel: " + pkt.BestStarLevel);
                HSMPClient.Get().SendDebugMessage("\tLegendIndex: " + pkt.LegendIndex);

                //HSMPClient.Get().Send(response);
            }
            public static void OnMedalHistory() {
                var pkt = ConnectAPI.GetMedalHistory();
                HSMPClient.Get().SendDebugMessage(pkt);
                foreach (var m in pkt.Medals) {
                    HSMPClient.Get().SendDebugMessage(m);
                }
            }
        }
    }
}
