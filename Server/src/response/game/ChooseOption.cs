using HSInfo;

namespace HSClient {
    namespace Gameplay {
        public class ChooseOption {
            public static void OnMessageRecv(Message msg) {
                var choice = (MsgChooseOption)msg;
                GameState gs = GameState.Get();
                if (gs == null)
                    return;
                
                gs.SetSelectedOption(choice.Main);
                gs.SetSelectedOptionPosition(choice.Position);
                gs.SetSelectedOptionTarget(choice.Target);
                gs.SetSelectedSubOption(choice.Sub);
                gs.SendOption();
            }
        }
    }
}
