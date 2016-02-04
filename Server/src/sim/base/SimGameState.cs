using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    public class SimGameState {
        /* --- Constructors --- */
        public SimGameState() {
            EntityMap = new Map<int, SimEntity>();
            PlayerMap = new Map<int, SimPlayer>();
        }
        /* --- Instance Methods (Interface) --- */
        public void AddEntity(SimEntitySpec spec) {
            if (EntityMap.ContainsKey(spec.ID)) {
                Console.WriteLine("ERROR - SimGame::AddEntity(SimEntitySpec e) => already contains id (" + spec.ID + ")");
                return;
            }
            EntityMap.Add(spec.ID, new SimEntity(spec));
        }
        public void Reset() {
            EntityMap.Clear();
            PlayerMap.Clear();
        }
        public void AddPlayer(SimPlayerSpec spec) {
            var player = new SimPlayer(spec);
            PlayerMap.Add(player.ID, player);
            EntityMap.Add(player.ID, player);
        }
        public SimEntity GetEntity(int id) {
            if (EntityMap.ContainsKey(id) == false) {
                Console.WriteLine("Entity with ID (" + id + ") not found...");
                return null;
            }
            return EntityMap[id];
        }
        /* --- Instance Properties --- */
        public Map<int, SimEntity> EntityMap { get; set; }
        public Map<int, SimPlayer> PlayerMap { get; set; }
    }
}