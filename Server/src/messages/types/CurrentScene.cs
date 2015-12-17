

using System.IO;

namespace HSInfo {
    namespace MessageTypes { 
        public partial class CurrentScene : Message {

            public const int HUB_SCENE = 0;
            public const int TOURNAMENT_SCENE = 1;
            public const int ADVENTURE_SCENE = 2;
            public const int GAMEPLAY = 3;

            public CurrentScene() : base(ID) { }
            public CurrentScene(int id) : base(ID) { m_id = id; }
            public override void Deserialize(BinaryReader r) {
                m_id = r.ReadInt32();
            }
            public override void Serialize(BinaryWriter w) {
                w.Write(m_id);
            }
            public int m_id;

            public static int NameToID(string name) {
                int id = -1;
                if (name == "Hub")
                    id = HUB_SCENE;
                else if (name == "Tournament")
                    id = TOURNAMENT_SCENE;
                else if (name == "AdventureScene")
                    id = ADVENTURE_SCENE;
                else if (name == "Gameplay")
                    id = GAMEPLAY;
                return id;
            }

            public static string IDToName(int id) {
                switch (id) {
                    case HUB_SCENE:
                        return "Hub";
                    case TOURNAMENT_SCENE:
                        return "Tournament";
                    case ADVENTURE_SCENE:
                        return "Adventure";
                    case GAMEPLAY:
                        return "Gameplay";
                    default:
                        return "Invalid";
                }
            }

        }
    }
}
