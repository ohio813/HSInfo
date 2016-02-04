
using HSInfo;

namespace HSClient {
    namespace Account {
        public static class ResponseInstaller {
            public static void Register(HSMPClient c) {
                c.RegisterListener(MsgAccountInfoRequest.ID, RequestAccountInfoHandler.OnMessageRecv);
            }
        }
    }
}
