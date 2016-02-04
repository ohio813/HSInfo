
using HSClient;
using System.IO;

namespace HSInfo {
    public partial class MsgTaskSet : Message {
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            TaskType = (BotTask.Task)r.ReadInt32();
            switch (TaskType) {
                case BotTask.Task.PLAY_INNKEEPER:

                    break;
                case BotTask.Task.PLAY_TOURNAMENT:
                    TaskParams = new TaskParamPlayTournament();
                    break;
                case BotTask.Task.PLAY_TAVERN_BRAWL:

                    break;
            }
            TaskParams.Deserialize(r);
        }
        public override void Serialize(BinaryWriter w) {
            w.Write((int)TaskType);
            TaskParams.Serialize(w);
        }
        /* --- Instance Fields --- */
        public BotTask.Task TaskType;
        public ISerializable TaskParams;
    }
}
