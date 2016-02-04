using HSInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSServer {
    public class BotHandler {
        /* --- Constructors --- */
        public BotHandler() {
            m_simGameState = new SimGameState();
            HSMPServer.Get().RegisterListener(MsgOptions.ID, OnMsgOptions);
            HSMPServer.Get().RegisterListener(MsgPowerHistory.ID, OnMsgPowerHistory);
        }
        /* --- Instance Methods (Auxiliary) --- */
        private void OnMsgOptions(Message msg) {
            var options = (MsgOptions)msg;
            MsgChooseOption response;

            if (options.Options.Count == 1) {
                response = new MsgChooseOption(0, 0, 0, -1);
            } else {
                var r = new Random();
                int i = 1 + (r.Next() % (options.Options.Count - 1));
                var o = options.Options[i];

                response = new MsgChooseOption();
                response.Main = i;
                response.Position = 0;
                response.Target = o.Main.Targets == null ? 0 : o.Main.Targets[r.Next() % o.Main.Targets.Count];
                response.Sub = -1;
                if (o.Subs != null && o.Subs.Count > 0) {
                    response.Sub = r.Next() % o.Subs.Count;
                    if (o.Subs[response.Sub].Targets != null && o.Subs[response.Sub].Targets.Count > 0) {
                        response.Target = o.Subs[response.Sub].Targets[r.Next() % o.Subs[response.Sub].Targets.Count];
                    }
                }
            }

            HSMPServer.Get().Send(response);
        }
        private void OnMsgPowerHistory(Message msg) {
            var ph = (MsgPowerHistory)msg;
            foreach (var p in ph.PowerHistory) {
                switch (p.Type) {
                    case SimPowerType.SHOW_ENTITY:
                        OnShowEntity((SimShowEntity)p);
                        break;
                    case SimPowerType.FULL_ENTITY:
                        OnFullEntity((SimFullEntity)p);
                        break;
                    case SimPowerType.TAG_CHANGE:
                        OnTagChange((SimTagChange)p);
                        break;
                    case SimPowerType.HIDE_ENTITY:
                        OnHideEntity((SimHideEntity)p);
                        break;
                    case SimPowerType.CREATE_GAME:
                        OnCreateGame((SimCreateGame)p);
                        break;
                    case SimPowerType.ACTION_START:
                        OnActionStart((SimActionStart)p);
                        break;
                    case SimPowerType.ACTION_END:
                        break;
                    case SimPowerType.META_DATA:
                        break;
                }
            }
        }
        private void OnCreateGame(SimCreateGame e) {
            m_simGameState.Reset();
            m_simGameState.AddEntity(e.GameSpec);
            foreach (var p in e.PlayerSpecs) {
                m_simGameState.AddPlayer(p);
            }
        }
        private void OnActionStart(SimActionStart e) {
        //    Console.WriteLine(e.BlockType + " : " + e.Index + " : " + e.EntityID);
        }
        private void OnShowEntity(SimShowEntity e) {
            int id = e.EntitySpec.ID;
            var ent = m_simGameState.GetEntity(id);
            if (ent == null)
                return;
            ent.CardID = e.EntitySpec.CardID;
            ent.Tags.SetTags(e.EntitySpec.Tags);
        }
        private void OnHideEntity(SimHideEntity e) {
            int id = e.EntityID;
            var ent = m_simGameState.GetEntity(id);
            if (ent == null)
                return;
            // TODO...???
        }
        private void OnFullEntity(SimFullEntity e) {
            m_simGameState.AddEntity(e.EntitySpec);
        }
        private void OnTagChange(SimTagChange e) {
            int id = e.EntityID;
            var ent = m_simGameState.GetEntity(id);
            if (ent == null)
                return;
            ent.Tags.SetTag(e.TagName, e.TagValue);
        }
        /* --- Instance Fields --- */
        private SimGameState m_simGameState;
    }
}
