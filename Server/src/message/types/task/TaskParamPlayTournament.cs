
using System.IO;

namespace HSInfo {
    public class TaskParamPlayTournament : ISerializable {
        public TaskParamPlayTournament() { }
        public TaskParamPlayTournament(int deckIndex, bool ranked) {
            DeckIndex = deckIndex;
            Ranked = ranked;
        }
        public void Deserialize(BinaryReader r) {
            DeckIndex = r.ReadInt32();
            Ranked = r.ReadBoolean();
        }
        public void Serialize(BinaryWriter w) {
            w.Write(DeckIndex);
            w.Write(Ranked);
        }
        public int DeckIndex;
        public bool Ranked;
    }
}
