

using HSInfo;

namespace HSClient {
    public class HSMPClientHandler : UnityEngine.MonoBehaviour {
        /* --- Instance Methods (Interface) --- */
        public void Awake() {
            s_instance = this;
        }
        public void LateUpdate() {
            HSMPClient.Get().GetStream().CheckForMessages(MessageHandler.Alloc);
            HSMPClient.Get().GetStream().Distribute();
        }
        public void OnDestroy() {
            HSMPClient.Get().Send(new MsgStatus(MsgStatus.State.DISCONNECTED, MsgStatus.User.CLIENT, "NORMAL"));
            HSMPClient.Get().GetStream().Close();
        }
        /* --- Static Methods (Interface) --- */
        public static HSMPClientHandler Get() { return s_instance; }
        /* --- Static Fields --- */
        private static HSMPClientHandler s_instance;
    }
}