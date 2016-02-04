

using HSInfo;

namespace HSClient {
    public class BotTaskHandler : UnityEngine.MonoBehaviour {
        public void Awake() {
            HSMPClient.Get().RegisterListener(MsgTaskSet.ID, OnMsgTaskSet);
            HSMPClient.Get().RegisterListener(MsgTaskGetState.ID, OnMsgTaskGetState);
        }
        public void Update() {
            
        }
        public void OnMsgTaskSet(Message msg) {
            var taskSet = (MsgTaskSet)msg;
            var sceneMgr = SceneMgr.Get();
            if (sceneMgr.IsInGame()) {

            }
            
            switch (taskSet.TaskType) {
                case BotTask.Task.PLAY_TOURNAMENT:
                    OnTaskPlayTournament((TaskParamPlayTournament)taskSet.TaskParams);
                    break;
            }
        }
        public void OnMsgTaskGetState(Message msg) {

        }
        private bool OnTaskPlayTournament(TaskParamPlayTournament p) {
            PegasusShared.GameType type = p.Ranked ? 
                PegasusShared.GameType.GT_RANKED : 
                PegasusShared.GameType.GT_UNRANKED;

            long deckID = GetDeckID(p.DeckIndex);
            if (deckID == 0) {
                HSMPClient.Get().SendDebugMessage("invalid deck ID :(");
                return false;
            }

            GameMgr.Get().FindGame(type, 2, deckID, 0);
            return true;
        }
        private long GetDeckID(int index) {
            var cm = CollectionManager.Get();
            var decks = cm.GetDecks();
            int i = 0;
            foreach (var s in decks) {
                if (i++ == index) {
                    return s.Value.ID;
                }
            }
            return 0;
        }
    }
}