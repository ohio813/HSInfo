
using System;
using System.Collections.Generic;
using System.IO;

namespace HSInfo {
    public partial class MsgPowerHistory : Message {
        /* --- Constructors --- */
        public MsgPowerHistory(List<Network.PowerHistory> l) : base(ID) {
            m_powerHistory = l;
        }
        /* --- Instance Methods (Interface) --- */
        public override void Deserialize(BinaryReader r) {
            var list = new List<SimPowerHistory>();
            int c = r.ReadInt32();
            for (int i = 0; i < c; ++i) {
                var k = (SimPowerType)r.ReadInt32();
                SimPowerHistory h = null;
                switch (k) {
                    case SimPowerType.ACTION_END:
                        h = new SimActionEnd();
                        break;
                    case SimPowerType.ACTION_START:
                        h = new SimActionStart(r);
                        break;
                    case SimPowerType.CREATE_GAME:
                        h = new SimCreateGame(r);
                        break;
                    case SimPowerType.FULL_ENTITY:
                        h = new SimFullEntity(r);
                        break;
                    case SimPowerType.HIDE_ENTITY:
                        h = new SimHideEntity(r);
                        break;
                    case SimPowerType.META_DATA:
                        h = new SimMetaData(r);
                        break;
                    case SimPowerType.SHOW_ENTITY:
                        h = new SimShowEntity(r);
                        break;
                    case SimPowerType.TAG_CHANGE:
                        h = new SimTagChange(r);
                        break;
                }
                list.Add(h);
            }
            PowerHistory = list;
        }
        public override void Serialize(BinaryWriter w) {
            w.Write(m_powerHistory.Count);
            foreach (var ph in m_powerHistory) {
                w.Write((int)ph.Type);
                switch (ph.Type) {
                    case Network.PowerType.FULL_ENTITY:
                        var fe = (Network.HistFullEntity)ph;
                        SerializeEntity(fe.Entity, w);
                        break;
                    case Network.PowerType.HIDE_ENTITY:
                        var he = (Network.HistHideEntity)ph;
                        w.Write(he.Entity);
                        w.Write(he.Zone);
                        break;
                    case Network.PowerType.SHOW_ENTITY:
                        var se = (Network.HistShowEntity)ph;
                        SerializeEntity(se.Entity, w);
                        break;
                    case Network.PowerType.TAG_CHANGE:
                        var tc = (Network.HistTagChange)ph;
                        w.Write(tc.Entity);
                        w.Write(tc.Tag);
                        w.Write(tc.Value);
                        break;
                    case Network.PowerType.CREATE_GAME:
                        var cg = (Network.HistCreateGame)ph;
                        SerializeCreateGame(cg, w);
                        break;
                    case Network.PowerType.META_DATA:
                        SerializeMetaData((Network.HistMetaData)ph, w);
                        break;
                    case Network.PowerType.ACTION_START:
                        SerializeActionStart((Network.HistActionStart)ph, w);
                        break;
                    case Network.PowerType.ACTION_END:
                        break;
                }
            }
        }
        /* --- Instance Methods (Auxiliary) --- */
        private void SerializeMetaData(Network.HistMetaData d, BinaryWriter w) {
            w.Write((int)d.MetaType);
            w.Write(d.Data);
            w.Write(d.Info.Count);
            foreach (var i in d.Info) {
                w.Write(i);
            }
        }
        private void SerializeActionStart(Network.HistActionStart a, BinaryWriter w) {
            w.Write(a.Index);
            w.Write(a.Entity);
            w.Write(a.Target);
            w.Write((int)a.BlockType);
        }
        private void SerializeEntity(Network.Entity e, BinaryWriter w) {
            w.Write(e.ID);
            w.Write(e.CardID);
            SerializeTags(e.Tags, w);
        }
        private void SerializeTags(List<Network.Entity.Tag> tags, BinaryWriter w) {
            w.Write(tags.Count);
            foreach (var t in tags) {
                w.Write(t.Name);
                w.Write(t.Value);
            }
        }
        private void SerializeCreateGame(Network.HistCreateGame cg, BinaryWriter w) {
            SerializeEntity(cg.Game, w);
            w.Write(cg.Players.Count);
            foreach (var p in cg.Players) {
                SerializeEntity(p.Player, w);
                w.Write(p.ID);
                w.Write(IsLocal(p));
            }
        }
        private bool IsLocal(Network.HistCreateGame.PlayerData p) {
            return p.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId();
        }
        /* --- Instance Fields --- */
        private List<Network.PowerHistory> m_powerHistory;
        public List<SimPowerHistory> PowerHistory { get; private set; }
    }
}
