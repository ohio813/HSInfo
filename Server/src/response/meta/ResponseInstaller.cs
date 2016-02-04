
/*
 * File: response/meta/ResponseInstaller.cs
 * Notes:
 */

using HSInfo;

namespace HSClient {
    namespace Meta {
        public static class ResponseInstaller {
            public static void Register(HSMPClient c) {
                c.RegisterListener(MsgDebug.ID, DebugHandler.OnMessageRecv);
            }
        }
    }
}
