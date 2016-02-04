
using System.IO;

namespace HSInfo {
    public partial class MsgStatus : Message {
        /* --- Inner Types --- */
        public enum State : int {
            CONNECTED,
            HANDSHAKE_FAILED,
            DISCONNECTED
        }
        public enum User : int {
            CLIENT,
            SERVER
        }
        /* --- Constructors --- */
        public MsgStatus(State state, User user, string msg) : base(ID) {
            Status = state;
            m_user = user;
            Message = msg;
        }
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            Status = (State)r.ReadInt32();
            m_user = (User)r.ReadInt32();
            Message = r.ReadString();
        }
        public override void Serialize(BinaryWriter w) {
            w.Write((int)Status);
            w.Write((int)m_user);
            w.Write(Message);
        }
        public bool IsClient() { return m_user == User.CLIENT; }
        public bool IsServer() { return m_user == User.SERVER; }
        /* --- Properties --- */
        public State Status { get; set; }
        public string Message { get; set; }
        /* --- Instance Fields --- */
        private User m_user;
    }
}
