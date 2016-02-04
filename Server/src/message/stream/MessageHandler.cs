
using HSInfo;

namespace HSInfo {
    public static class MessageHandler {
        public static Message Alloc(int id) {
            switch (id) {
                /* --- Base Messages --- */
                case MsgDebug.ID:
                    return new MsgDebug();
                case MsgStatus.ID:
                    return new MsgStatus();
                /* --- Game Messages --- */
                case MsgChooseOption.ID:
                    return new MsgChooseOption();
                case MsgOptions.ID:
                    return new MsgOptions();
                case MsgPowerHistory.ID:
                    return new MsgPowerHistory();
                /* --- Task Messages --- */
                case MsgTaskFinished.ID:
                    return new MsgTaskFinished();
                case MsgTaskSet.ID:
                    return new MsgTaskSet();
                /* --- Account Messages --- */
                case MsgCurrencyBalance.ID:
                    return new MsgCurrencyBalance();
                case MsgAccountInfoRequest.ID:
                    return new MsgAccountInfoRequest();
                case MsgAccountInfoResponse.ID:
                    return new MsgAccountInfoResponse();
                case MsgOpenedPack.ID:
                    return new MsgOpenedPack();
                case MsgDeckList.ID:
                    return new MsgDeckList();
            }
            return null;
        }
    }
}
