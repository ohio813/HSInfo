
/*
 * File: response/meta/DebugHandler.cs
 * Notes:
 */

using HSInfo;
using System.Reflection;
using UnityEngine;

namespace HSClient {
    namespace Meta {
        public class DebugHandler {
            public static void OnMessageRecv(Message msg) {
                var gs = GameState.Get();
                gs.SendChoices();
                //MulliganManager.Get().GetMulliganButton().TriggerRelease();
                MulliganManager.Get().EndMulligan();

                //MulliganManager.Get().EndMulligan();

                //GameMgr.Get().FindGame(PegasusShared.GameType.GT_UNRANKED, 2, deckID, 0);
                //GameMgr.Get().CancelFindGame();

                //GameMgr.Get().FindGame(PegasusShared.GameType.GT_UNRANKED, 2, deckID, 0);
                //SceneMgr.Get().SetNextMode(SceneMgr.Mode.ADVENTURE);

                //gs.SetSelectedOption(1);
                //gs.SetSelectedOptionPosition(1);
                //gs.SendOption();
            }
        }
    }
}
