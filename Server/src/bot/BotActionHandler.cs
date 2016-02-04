

using HSInfo;

namespace HSClient {
    public class BotActionHandler : UnityEngine.MonoBehaviour {
        public void Awake() {
            HSMPClient.Get().RegisterListener(MsgChooseOption.ID, OnMsgChooseOption);
            Network.Get().RegisterNetHandler(PegasusGame.NAckOption.PacketID.ID, OnPacketNAckOption);
        }
        public void Update() {
            GameState gs = GameState.Get();
            if (gs == null)
                return;
            var rah = RemoteActionHandler.Get();
            
            if (gs.IsMulliganManagerActive()) {
                // TODO: set choices...
                MulliganManager.Get().GetMulliganButton().TriggerRelease();
            }

            if (gs.IsFriendlySidePlayerTurn()) {
                if (gs.IsTurnStartManagerBlockingInput() == false) {
                    if (m_nextChooseOption == null)
                        return;
                    SendOption(m_nextChooseOption);
                    m_nextChooseOption = null;
                }
            }
        }
        private void OnPacketNAckOption() {
            HSMPClient.Get().SendDebugMessage("NAck Option...");
        }
        private void SendOption(MsgChooseOption o) {
            var gs = GameState.Get();
            gs.SetSelectedOption(o.Main);
            gs.SetSelectedOptionPosition(o.Position);
            gs.SetSelectedOptionTarget(o.Target);
            gs.SetSelectedSubOption(o.Sub);
            gs.SendOption();
        }
        private void OnMsgChooseOption(Message msg) {
            var chooseOption = (MsgChooseOption)msg;
            SetNextOption(chooseOption);
        }
        private void SetNextOption(MsgChooseOption o) {
            m_nextChooseOption = o;
        }
        private MsgChooseOption m_nextChooseOption;
        private Card m_currentHover;
    }
}