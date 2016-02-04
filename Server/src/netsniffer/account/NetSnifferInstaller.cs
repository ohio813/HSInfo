
namespace HSClient {
    namespace Account {
        public class NetSnifferInstaller {
            /* --- Static Methods (Interface) --- */
            public static void Register() {
                if (s_installed)
                    return;

                var net = Network.Get();

                // Gold
                net.RegisterNetHandler(PegasusUtil.GoldBalance.PacketID.ID, new Network.NetHandler(GoldNetSniffer.OnGoldBalance));
                net.RegisterNetHandler(PegasusUtil.PurchaseWithGoldResponse.PacketID.ID, new Network.NetHandler(GoldNetSniffer.OnPurchaseWithGoldResponse));

                // Dust 
                net.RegisterNetHandler(PegasusUtil.ArcaneDustBalance.PacketID.ID, new Network.NetHandler(DustNetSniffer.OnArcaneDustBalance));
                net.RegisterNetHandler(PegasusUtil.BoughtSoldCard.PacketID.ID, new Network.NetHandler(DustNetSniffer.OnBoughtSoldCard));

                // Boosters
                net.RegisterNetHandler(PegasusUtil.BoosterList.PacketID.ID, new Network.NetHandler(BoosterNetSniffer.OnBoosterList));
                net.RegisterNetHandler(PegasusUtil.BoosterTallyList.PacketID.ID, new Network.NetHandler(BoosterNetSniffer.OnBoosterTally));
                net.RegisterNetHandler(PegasusUtil.BoosterContent.PacketID.ID, new Network.NetHandler(BoosterNetSniffer.OnBoosterContent));

                // Rank
                net.RegisterNetHandler(PegasusUtil.MedalInfo.PacketID.ID, new Network.NetHandler(RankNetSniffer.OnMedalInfo));

                // Rewards
                net.RegisterNetHandler(PegasusUtil.ProfileNotices.PacketID.ID, new Network.NetHandler(RewardNetSniffer.OnProfileNotices));

                s_installed = true;
            }
            /* --- Static Fields --- */
            private static bool s_installed;
        }
    }
}
