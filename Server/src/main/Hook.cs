
using HSInfo;

namespace HSClient {
    public class Hook {
        /* --- Static Methods (Interface) --- */
        public static void Load() {
            
            // connect to the bot server
            if (HSMPClient.Get() == null) {
                HSMPClient.Connect("foobar");
            }
           
            if (NetCache.Get() == null || BnetPresenceMgr.Get() == null || BnetPresenceMgr.Get().GetMyPlayer() == null) {
                HSMPClient.Get().Send(new MsgStatus(MsgStatus.State.HANDSHAKE_FAILED, MsgStatus.User.CLIENT, "Hearthstone is initializing, please wait..."));
                return;
            }
            
            if (s_loaded == false) {
                // create root game object
                UnityEngine.GameObject root = new UnityEngine.GameObject("HookRoot");
                root.transform.parent = BaseUI.Get().transform;

                // initialize subcomponents
                AttachScripts(root);
                InstallNetworkSniffers(root);
                RegisterMessageListeners(root);

                s_loaded = true;
            }

            HSMPClient.Get().Send(new MsgStatus(MsgStatus.State.CONNECTED, MsgStatus.User.CLIENT, "Success"));
           
        }
        /* --- Static Methods (Auxiliary) --- */
        private static void AttachScripts(UnityEngine.GameObject root) {
            root.AddComponent<HSMPClientHandler>();
            root.AddComponent<BotInfoDisplay>();
            root.AddComponent<BotTaskHandler>();
            root.AddComponent<BotActionHandler>();
        }
        private static void InstallNetworkSniffers(UnityEngine.GameObject root) {
            Account.NetSnifferInstaller.Register();
            Game.NetSnifferInstaller.Register();
        }
        private static void RegisterMessageListeners(UnityEngine.GameObject root) {
            HSMPClient c = HSMPClient.Get();
            Meta.ResponseInstaller.Register(c);
            Game.ResponseInstaller.Register(c);
            Account.ResponseInstaller.Register(c);
        }
        /* --- Static Fields --- */
        private static bool s_loaded;
    }
}
