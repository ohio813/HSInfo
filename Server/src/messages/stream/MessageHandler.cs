

namespace HSInfo {
    public static class MessageHandler {
        public static Message Alloc(int id) {
            switch (id) {
                case MessageTypes.DebugMessage.ID:
                    return new MessageTypes.DebugMessage();
                case MessageTypes.SwitchInput.ID:
                    return new MessageTypes.SwitchInput();
                case MessageTypes.CurrentScene.ID:
                    return new MessageTypes.CurrentScene();
                case MessageTypes.SetMouseTo.ID:
                    return new MessageTypes.SetMouseTo();
            }
            return null;
        }
    }
}
