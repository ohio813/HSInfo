
namespace HSClient {
    namespace Game {
        public class NetSnifferInstaller {
            /* --- Static Methods (Interface) --- */
            public static void Register() {
                if (s_installed)
                    return;

                var net = Network.Get();

                net.RegisterNetHandler(PegasusGame.AllOptions.PacketID.ID, new Network.NetHandler(AllOptionsNetSniffer.OnAllOptions));
                //net.RegisterNetHandler(PegasusGame.EntitiesChosen.PacketID.ID, new Network.NetHandler(AllNetSniffer.OnEntitiesChosen));
                //net.RegisterNetHandler(PegasusGame.TurnTimer.PacketID.ID, new Network.NetHandler(AllNetSniffer.OnTurnTimer));
                net.RegisterNetHandler(PegasusGame.PowerHistory.PacketID.ID, new Network.NetHandler(AllNetSniffer.OnPowerHistory));
                net.RegisterNetHandler(PegasusGame.EntityChoices.PacketID.ID, AllNetSniffer.OnEntityChoices);
                s_installed = true;
            }
            /* --- Static Fields --- */
            private static bool s_installed;
        }
    }
}
