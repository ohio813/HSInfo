
/*
 * File: StandardGameEntityExt.cs
 * Notes:
 */

namespace HSInfo {
    namespace Server {
        public class StandardGameEntityExt : StandardGameEntity {
            /* --- Constructors --- */
            public StandardGameEntityExt(StandardGameEntity old) {
                // ... sketchy :O
                System.Reflection.PropertyInfo[] properties = old.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy);
                System.Reflection.FieldInfo[] fields = old.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy);
                foreach (System.Reflection.PropertyInfo prop in properties) {
                    try {
                        prop.SetValue(this, prop.GetValue(old, null), null);
                    } catch { }
                }
                foreach (System.Reflection.FieldInfo field in fields) {
                    field.SetValue(this, field.GetValue(old));
                }
            }
            /* --- Instance Methods (Interface) --- */
            public override void NotifyOfOpponentPlayedCard(Entity e) {
                base.NotifyOfOpponentPlayedCard(e);
                DialogManager.Get().ShowMessageOfTheDay(e.GetType().ToString());
                string msg = "Card Played: " + e.GetName();
                DialogManager.Get().ShowMessageOfTheDay(msg);
            }
            public override void NotifyOfTargetModeCancelled() {
                base.NotifyOfTargetModeCancelled();
                DialogManager.Get().ShowMessageOfTheDay("TargetModeCancelled");
            }
            public override void NotifyOfMulliganEnded() {
                base.NotifyOfMulliganEnded();
                DialogManager.Get().ShowMessageOfTheDay("MulliganEnded");
            }
            public override void NotifyOfManaCrystalSpawned() {
                base.NotifyOfManaCrystalSpawned();
                DialogManager.Get().ShowMessageOfTheDay("Mana Crystal Spawned");
            }
            public override void NotifyOfStartOfTurnEventsFinished() {
                base.NotifyOfStartOfTurnEventsFinished();
                DialogManager.Get().ShowMessageOfTheDay("Start of Turn Events Finished");
                foreach (Card c in GameState.Get().GetCurrentPlayer().GetBattlefieldZone().GetCards()) {
                    DialogManager.Get().ShowMessageOfTheDay(c.GetEntity().GetCardId());
                }
            }
        }
    }
}
