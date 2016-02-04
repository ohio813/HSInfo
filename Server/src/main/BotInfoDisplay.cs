
/*
 * File: BotInfoDisplay.cs
 * Notes:
 */

using HSInfo;
using UnityEngine;

namespace HSClient {
    public class BotInfoDisplay : MonoBehaviour {
        /* --- Instance Methods (Interface) --- */
        public void Awake() {
            m_serverState = MsgStatus.State.DISCONNECTED;
            HSMPClient.Get().RegisterListener(MsgStatus.ID, OnMsgStatus);
        }
        public void OnGUI() {
            switch (m_serverState) {
                case MsgStatus.State.CONNECTED:
                    GUI.Label(new Rect(8, 6, 200, 50), "[Bot Connected]");
                    break;
                case MsgStatus.State.DISCONNECTED:
                    GUI.Label(new Rect(8, 6, 200, 50), "[Bot Disconnected]");
                    break;
            }
        }
        public void OnMsgStatus(Message msg) {
            MsgStatus status = (MsgStatus)msg;
            m_serverState = status.Status;
        }
        /* --- Instance Fields --- */
        private MsgStatus.State m_serverState;
    }
}