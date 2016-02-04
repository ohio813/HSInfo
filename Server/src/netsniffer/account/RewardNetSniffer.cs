
/*
 * File: netsniffer/account/RewardNetSniffer.cs
 * Notes:
 */

using HSInfo;

namespace HSClient {
    namespace Account {
        public static class RewardNetSniffer {
            /* --- Static Methods (Interface) --- */
            public static void OnProfileNotices() {
                var pkt = ConnectAPI.GetProfileNotices();
                foreach (var n in pkt) {
                    HSMPClient.Get().SendDebugMessage("Reward: " + n.Type + " - " + n.Origin);
                    
                    switch (n.Type) {
                        case NetCache.ProfileNotice.NoticeType.REWARD_BOOSTER:
                            var n0 = (NetCache.ProfileNoticeRewardBooster)n;
                            var id = n0.Id;
                            var count = n0.Count;

                            break;
                        case NetCache.ProfileNotice.NoticeType.REWARD_CARD:
                            var n1 = (NetCache.ProfileNoticeRewardCard)n;
                            var cardID = n1.CardID;
                            var premium = n1.Premium;

                            break;
                        case NetCache.ProfileNotice.NoticeType.REWARD_DUST:
                            OnRewardDust((NetCache.ProfileNoticeRewardDust)n);
                            break;
                        case NetCache.ProfileNotice.NoticeType.REWARD_GOLD:
                            OnRewardGold((NetCache.ProfileNoticeRewardGold)n);
                            break;
                        case NetCache.ProfileNotice.NoticeType.GAINED_MEDAL:
                            OnRewardMedal((NetCache.ProfileNoticeMedal)n);
                            break;
                        case NetCache.ProfileNotice.NoticeType.DISCONNECTED_GAME:
                            break;
                        case NetCache.ProfileNotice.NoticeType.HERO_LEVEL_UP:
                            break;
                        case NetCache.ProfileNotice.NoticeType.PURCHASE:
                            break;
                        case NetCache.ProfileNotice.NoticeType.REWARD_FORGE:
                            var n5 = (NetCache.ProfileNoticeRewardForge)n;
                            HSMPClient.Get().SendDebugMessage("RewardForge: " + n5.Quantity);
                            break;
                    }
                }
            }
            /* --- Static Methods (Auxiliary) --- */
            private static void OnRewardDust(NetCache.ProfileNoticeRewardDust n) {
                HSMPClient.Get().SendDebugMessage("OnRewardDust: " + n.Amount);
            }
            private static void OnRewardGold(NetCache.ProfileNoticeRewardGold n) {
                HSMPClient.Get().SendDebugMessage("OnRewardGold: " + n.Amount);
            }
            private static void OnRewardMedal(NetCache.ProfileNoticeMedal n) {
                HSMPClient.Get().SendDebugMessage("Chest: " + n.Chest);
                HSMPClient.Get().SendDebugMessage("Star Level: " + n.StarLevel);
            }
        }
    }
}
