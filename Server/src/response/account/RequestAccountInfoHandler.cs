
using HSInfo;
using System.Collections.Generic;

namespace HSClient {
    namespace Account {
        public class RequestAccountInfoHandler {
            public static void OnMessageRecv(Message msg) {
                var response = new MsgAccountInfoResponse();

                var p = BnetPresenceMgr.Get().GetMyPlayer();
                string name = p.GetBattleTag().GetName();
                string region = BattleNet.GetCurrentRegion().ToString().Substring(7);
                response.Name = name + "-" + region;

                response.Dust = NetCache.Get().GetNetObject<NetCache.NetCacheArcaneDustBalance>().Balance;
                response.Gold = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>().GetTotal();
                response.Packs = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>().GetTotalNumBoosters();

                var rm = RankMgr.Get();
                var rpf = rm.GetRankPresenceField(p);
                var medal = rpf.GetCurrentMedal();

                response.Rank = medal.rank;
                response.Stars = medal.earnedStars;

                HSMPClient.Get().Send(response);

                // TEST

                var deckList = new MsgDeckList();
                var list = new List<MsgDeckList.Deck>();

                var cm = CollectionManager.Get();
                foreach (var k in cm.GetDecks()) {
                    var deck = new MsgDeckList.Deck();
                    deck.ID = k.Value.ID;
                    deck.Name = k.Value.Name;
                    deck.HeroClass = k.Value.GetClass();
                    list.Add(deck);
                }

                deckList.Decks = list;
                HSMPClient.Get().Send(deckList);
            }
        }
    }
}
