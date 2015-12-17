
/*
 * File: Hook.cs
 * Notes:
 */

using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.InteropServices;

namespace HSInfo {
    namespace Server {
        public class Hook {
            static UnityEngine.Vector3 s_vec = new UnityEngine.Vector3(1, 2, 3);

            static string[] s_temp = new string[1024];

            /* --- Static Methods (Interface) --- */
            static void OnGameStateInit(GameState gs, object userData) {
                string msg = "OnGameStateInit: " + gs.GetHashCode();
                DialogManager.Get().ShowMessageOfTheDay(msg);

                gs.RegisterCreateGameListener(s_gameCreateCallback);

                gs.RegisterGameOverListener(s_gameOverCallback);

            }
            static void OnSceneLoaded(SceneMgr.Mode mode, Scene scene, object userData) {
                string msg = "Scene Loaded: " + scene.name + scene.GetType();
                DialogManager.Get().ShowMessageOfTheDay(msg);
                s_server.Write(new MessageTypes.CurrentScene(MessageTypes.CurrentScene.NameToID(scene.name)));
            }
            static void OnCreateGame(GameState.CreateGamePhase phase, object userData) {
                string msg = "OnCreateGame: " + phase.ToString();
                DialogManager.Get().ShowMessageOfTheDay(msg);
                if (phase == GameState.CreateGamePhase.CREATED) {
                    var prop = GameState.Get().GetType().GetField("m_gameEntity", System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Instance);
                    
                    var sge = new StandardGameEntityExt((StandardGameEntity)GameState.Get().GetGameEntity());
                    prop.SetValue(GameState.Get(), sge);
                    //gs.AddEntity(sge);
                    
                }
            }
            static void OnFriendlyMinionDeath(float f) {
                DialogManager.Get().ShowMessageOfTheDay("OnFriendlyMinionDeath: " + f);
            }

            static void OnFriendlyHeroDamage(float f) {                
                // DialogManager.Get().ShowMessageOfTheDay("OnFriendlyHeroDamage: " + f);
            }

            class Bob : IConnectionListener<PegasusPacket> {
                public void PacketReceived(PegasusPacket p, object state) { 
                    if (p.Type != (int)PegasusGame.Pong.PacketID.ID) {
                        //DialogManager.Get().ShowMessageOfTheDay("Pkt Recv (" + p.Type + "): " + p.GetBody().ToString());
                        switch (p.Type) {
                            case (int)PegasusGame.AllOptions.PacketID.ID:
                                PegasusGame.AllOptions ao = (PegasusGame.AllOptions)p.GetBody();
                                //DialogManager.Get().ShowMessageOfTheDay("ID: " + ao.Id);
                                break;
                            case (int)PegasusGame.PowerHistory.PacketID.ID:

                                //s_server.Write(new MessageTypes.HSPacket(p));

                                PegasusGame.PowerHistory ph = (PegasusGame.PowerHistory)p.GetBody();
                                //foreach (PegasusGame.PowerHistoryData d in ph.List) {
                                 //   if (d.HasTagChange) {
                                 //       string msg = "HasTagChange (" + d.TagChange.Entity + ", " + d.TagChange.Tag + ", " + d.TagChange.Value + ")";                                        DialogManager.Get().ShowMessageOfTheDay("HasTagChange");
                                 //       msg += " " + s_temp[d.TagChange.Tag];
                                 //       DialogManager.Get().ShowMessageOfTheDay(msg);
                                  //  }
                                  //  if (d.HasShowEntity) {
                                 //       DialogManager.Get().ShowMessageOfTheDay("HasShowEntity");
                                 //   }
                                //}
                                break;
                            case (int)PegasusGame.SpectatorNotify.PacketID.ID:
                                PegasusGame.SpectatorNotify sn = (PegasusGame.SpectatorNotify)p.GetBody();
                                if (sn.HasChooseOption) {
                                    string msg = "ChooseOption: " + sn.ChooseOption.Target + " " + sn.ChooseOption.Id;
                                   // DialogManager.Get().ShowMessageOfTheDay(msg);
                                }
                                if (sn.HasSpectatorRemoved)
                                    DialogManager.Get().ShowMessageOfTheDay("HasSpectatorRemoved");
                                if (sn.HasSpectatorPasswordUpdate)
                                    DialogManager.Get().ShowMessageOfTheDay("HasSpectatorPasswordUpdate");
                                break;
                        }
                    }
                }
            }

            public static void OnGameOver(object obj) {
                DialogManager.Get().ShowMessageOfTheDay(obj.ToString());
            }
       
            class MyListener : MessageListener {
                public override void OnMessageRecv(Message m) {
                    //DialogManager.Get().ShowMessageOfTheDay("Message Recv (" + m.GetID() + ")");
                    switch (m.GetID()) {
                        case MessageTypes.DebugMessage.ID:
                            //DialogManager.Get().ShowMessageOfTheDay(((MessageTypes.Debug)m).m_msg);
                            s_server.Write(new MessageTypes.DebugMessage("Hello to you too (^:"));

                            Card c = GameState.Get().GetCurrentPlayer().GetHandZone().GetFirstCard();
                            var s = c.GetActor().GetMeshRenderer().GetComponent<UnityEngine.Collider>();
                            var v1 = Camera.current.WorldToScreenPoint(s.bounds.min);
                            var v2 = Camera.current.WorldToScreenPoint(s.bounds.max);
                            //string msg = "vecs: min(" + v1.x + ", " + v1.y + ", " + v1.z + "), max(" + v2.x + ", " + v2.y + ", " + v2.z + ")";
                            //s_server.Write(new MessageTypes.Debug(msg));
                            
                            break;
                        case MessageTypes.SetMouseTo.ID:
                            var m2 = ((MessageTypes.SetMouseTo)m);
                            Input.s_simMouseVec.Set(m2.m_x, m2.m_y, 0);
                            break;
                        case MessageTypes.SwitchInput.ID:
                            UnityEngine.Input.m_simActive = !UnityEngine.Input.m_simActive;
                            break;
                        case MessageTypes.CurrentScene.ID:
                            string name = SceneMgr.Get().GetScene().name;
                            int scene = MessageTypes.CurrentScene.NameToID(name);
                            s_server.Write(new MessageTypes.CurrentScene(scene));
                            break;
                    }
                }
            }

            static void OnTimedEvent(System.Object src, System.Timers.ElapsedEventArgs e) {
                float x = Input.s_simMouseVec.x;
                float y = Input.s_simMouseVec.y;
                //Input.s_simMouseVec.Set(500, y-10, 0);
                //DialogManager.Get().ShowMessageOfTheDay("(" + x + ", " + y + ")");
                s_server.CheckForMessages(MessageHandler.Alloc);
                s_server.Distribute();
                //s_server.Write(new Bar(42));
            }

            static System.Timers.Timer s_t;

            private static void ShowVec(string s, UnityEngine.Vector3 vec) {
                DialogManager.Get().ShowMessageOfTheDay(s + ": (" + vec.x + ", " + vec.y + ")");
            }

            public static void MoveSimMouseTo(PegUIElement b) {
                var s = b.gameObject.GetComponent<Collider>();
                var k = Camera.current.WorldToScreenPoint(s.bounds.center);
                Input.s_simMouseVec.Set(k.x, k.y, 0);
            }

            static ServerMessageStream s_server;
            
            public static void Load() {

                //DialogManager.Get().ShowMessageOfTheDay("" + s.bounds.size.x + " : " + s.bounds.size.y);

                //Input.m_simActive = true;
                //MoveSimMouseTo(Box.Get().m_StoreButton);

                build();

                s_server = ServerMessageStream.Create("foobar", 4096);
                s_server.RegisterListener(new MyListener());

                DialogManager.Get().ShowMessageOfTheDay("Bot Connected!");

               // Input.m_simActive = true;

                s_t = new System.Timers.Timer(250);
                s_t.Elapsed += OnTimedEvent;
                s_t.AutoReset = true;
                s_t.Enabled = true;

                // DEBUG
                SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
                
                GameState.GameStateInitializedCallback s_gameStateInitCallback = new GameState.GameStateInitializedCallback(OnGameStateInit);
                GameState.RegisterGameStateInitializedListener(s_gameStateInitCallback);

                s_gameCreateCallback = new GameState.CreateGameCallback(OnCreateGame);
                s_gameOverCallback = new GameState.GameOverCallback(OnGameOver);

                BoardEvents.Get().RegisterFriendlyMinionDeathEvent(OnFriendlyMinionDeath, 0);
                BoardEvents.Get().RegisterFriendlyHeroDamageEvent(OnFriendlyHeroDamage, 0);
                
                ConnectAPI.GetGameServerConnection().AddListener(new Bob(), null);
                
                foreach (var s in GameState.Get().GetEntityMap().Values) {
                    string msg = "Entity: ID(" + s.GetEntityId() + ") Name(" + s.GetName() + ") Zone(" + s.GetZone() + ")";
                    int b = s.GetTag(GAME_TAG.CANT_PLAY);
                    msg += "CANT_PLAY(" + b + ")";
                    s_server.Write(new MessageTypes.DebugMessage(msg));
                }

            }
            public static void Unload() {
                //s_hsInfo.close();
                
            }

            /* --- Static Fields --- */
            //static Server s_hsInfo;
            static GameState.CreateGameCallback s_gameCreateCallback;
            static GameState.GameOverCallback s_gameOverCallback;
            //static PipeServer s_server;
            /* --- Const Fields --- */
            const long s_mapSize = 4096;
            const string s_pipeName = "46305E15-CA34-4C83-B485-A8B3C90F6E5B";

            static void build() {
                s_temp[0x00000004] = "TAG_SCRIPT_DATA_ENT_1";
                s_temp[0x00000005] = "TAG_SCRIPT_DATA_ENT_2";
                s_temp[0x00000002] = "TAG_SCRIPT_DATA_NUM_1";
                s_temp[0x00000003] = "TAG_SCRIPT_DATA_NUM_2";
                s_temp[0x000000CC] = "STATE";
                s_temp[0x00000014] = "TURN";
                s_temp[0x00000013] = "STEP";
                s_temp[0x000000C6] = "MoveNext";
                s_temp[0x0000001F] = "TEAM_ID";
                s_temp[0x0000001E] = "PLAYER_ID";
                s_temp[0x0000001D] = "STARTHANDSIZE";
                s_temp[0x0000001C] = "MAXHANDSIZE";
                s_temp[0x000000B0] = "MAXRESOURCES";
                s_temp[0x00000007] = "TIMEOUT";
                s_temp[0x00000008] = "TURN_START";
                s_temp[0x00000009] = "TURN_TIMER_SLUSH";
                s_temp[0x00000195] = "HEROPOWER_ADDITIONAL_ACTIVATIONS";
                s_temp[0x00000196] = "HEROPOWER_ACTIVATIONS_THIS_TURN";
                s_temp[0x0000000D] = "GOLD_REWARD_STATE";
                s_temp[0x00000018] = "FIRST_PLAYER";
                s_temp[0x00000017] = "CURRENT_PLAYER";
                s_temp[0x0000001B] = "HERO_ENTITY";
                s_temp[0x0000001A] = "RESOURCES";
                s_temp[0x00000019] = "RESOURCES_USED";
                s_temp[0x00000016] = "FATIGUE";
                s_temp[0x00000011] = "PLAYSTATE";
                s_temp[0x00000123] = "CURRENT_SPELLPOWER";
                s_temp[0x00000131] = "MULLIGAN_STATE";
                s_temp[0x0000015C] = "HAND_REVEALED";
                s_temp[0x0000017F] = "STEADY_SHOT_CAN_TARGET";
                s_temp[0x000000B9] = "CARDNAME";
                s_temp[0x000000B8] = "CARDTEXT_INHAND";
                s_temp[0x000000C8] = "CARDRACE";
                s_temp[0x000000CA] = "CARDTYPE";
                s_temp[0x00000030] = "COST";
                s_temp[0x0000002D] = "HEALTH";
                s_temp[0x0000002F] = "ATK";
                s_temp[0x000000BB] = "DURABILITY";
                s_temp[0x00000124] = "ARMOR";
                s_temp[0x0000013E] = "PREDAMAGE";
                s_temp[0x00000145] = "TARGETING_ARROW_TEXT";
                s_temp[0x00000012] = "LAST_AFFECTED_BY";
                s_temp[0x0000014A] = "ENCHANTMENT_BIRTH_VISUAL";
                s_temp[0x0000014B] = "ENCHANTMENT_IDLE_VISUAL";
                s_temp[0x0000000C] = "PREMIUM";
                s_temp[0x00000035] = "ENTITY_ID";
                s_temp[0x00000034] = "DEFINITION";
                s_temp[0x00000033] = "OWNER";
                s_temp[0x00000032] = "CONTROLLER";
                s_temp[0x00000031] = "ZONE";
                s_temp[0x0000002B] = "EXHAUSTED";
                s_temp[0x00000028] = "ATTACHED";
                s_temp[0x00000027] = "PROPOSED_ATTACKER";
                s_temp[0x00000026] = "ATTACKING";
                s_temp[0x00000025] = "PROPOSED_DEFENDER";
                s_temp[0x00000024] = "DEFENDING";
                s_temp[0x00000023] = "PROTECTED";
                s_temp[0x00000022] = "PROTECTING";
                s_temp[0x00000021] = "RECENTLY_ARRIVED";
                s_temp[0x0000002C] = "DAMAGE";
                s_temp[0x00000020] = "TRIGGER_VISUAL";
                s_temp[0x00000152] = "TAG_ONE_TURN_EFFECT";
                s_temp[0x000000BE] = "TAUNT";
                s_temp[0x000000C0] = "SPELLPOWER";
                s_temp[0x000000C2] = "DIVINE_SHIELD";
                s_temp[0x000000C5] = "CHARGE";
                s_temp[0x000000DB] = "SECRET";
                s_temp[0x00000125] = "MORPH";
                s_temp[0x00000132] = "TAUNT_READY";
                s_temp[0x00000133] = "STEALTH_READY";
                s_temp[0x00000134] = "CHARGE_READY";
                s_temp[0x00000139] = "CREATOR";
                s_temp[0x000000E8] = "CANT_DRAW";
                s_temp[0x000000E7] = "CANT_PLAY";
                s_temp[0x000000E6] = "CANT_DISCARD";
                s_temp[0x000000E5] = "CANT_DESTROY";
                s_temp[0x000000E4] = "CANT_TARGET";
                s_temp[0x000000E3] = "CANT_ATTACK";
                s_temp[0x000000E2] = "CANT_EXHAUST";
                s_temp[0x000000E1] = "CANT_READY";
                s_temp[0x000000E0] = "CANT_REMOVE_FROM_GAME";
                s_temp[0x000000DF] = "CANT_SET_ASIDE";
                s_temp[0x000000DE] = "CANT_DAMAGE";
                s_temp[0x000000DD] = "CANT_HEAL";
                s_temp[0x000000F7] = "CANT_BE_DESTROYED";
                s_temp[0x000000F6] = "CANT_BE_TARGETED";
                s_temp[0x000000F5] = "CANT_BE_ATTACKED";
                s_temp[0x000000F4] = "CANT_BE_EXHAUSTED";
                s_temp[0x000000F3] = "CANT_BE_READIED";
                s_temp[0x000000F2] = "CANT_BE_REMOVED_FROM_GAME";
                s_temp[0x000000F1] = "CANT_BE_SET_ASIDE";
                s_temp[0x000000F0] = "CANT_BE_DAMAGED";
                s_temp[0x000000EF] = "CANT_BE_HEALED";
                s_temp[0x000000FD] = "CANT_BE_SUMMONING_SICK";
                s_temp[0x0000013A] = "CANT_BE_DISPELLED";
                s_temp[0x000000EE] = "INCOMING_DAMAGE_CAP";
                s_temp[0x000000ED] = "INCOMING_DAMAGE_ADJUSTMENT";
                s_temp[0x000000EC] = "INCOMING_DAMAGE_MULTIPLIER";
                s_temp[0x000000EB] = "INCOMING_HEALING_CAP";
                s_temp[0x000000EA] = "INCOMING_HEALING_ADJUSTMENT";
                s_temp[0x000000E9] = "INCOMING_HEALING_MULTIPLIER";
                s_temp[0x00000104] = "FROZEN";
                s_temp[0x00000105] = "JUST_PLAYED";
                s_temp[0x00000106] = "LINKEDCARD";
                s_temp[0x00000107] = "ZONE_POSITION";
                s_temp[0x00000108] = "CANT_BE_FROZEN";
                s_temp[0x0000010A] = "COMBO_ACTIVE";
                s_temp[0x0000010B] = "CARD_TARGET";
                s_temp[0x0000010D] = "NUM_CARDS_PLAYED_THIS_TURN";
                s_temp[0x0000010E] = "CANT_BE_TARGETED_BY_OPPONENTS";
                s_temp[0x0000010F] = "NUM_TURNS_IN_PLAY";
                s_temp[0x000000CD] = "SUMMONED";
                s_temp[0x000000D4] = "ENRAGED";
                s_temp[0x000000BC] = "SILENCED";
                s_temp[0x000000BD] = "WINDFURY";
                s_temp[0x000000D8] = "LOYALTY";
                s_temp[0x000000D9] = "DEATHRATTLE";
                s_temp[0x0000015E] = "ADJACENT_BUFF";
                s_temp[0x000000BF] = "STEALTH";
                s_temp[0x000000DA] = "BATTLECRY";
                s_temp[0x00000110] = "NUM_TURNS_LEFT";
                s_temp[0x00000111] = "OUTGOING_DAMAGE_CAP";
                s_temp[0x00000112] = "OUTGOING_DAMAGE_ADJUSTMENT";
                s_temp[0x00000113] = "OUTGOING_DAMAGE_MULTIPLIER";
                s_temp[0x00000114] = "OUTGOING_HEALING_CAP";
                s_temp[0x00000115] = "OUTGOING_HEALING_ADJUSTMENT";
                s_temp[0x00000116] = "OUTGOING_HEALING_MULTIPLIER";
                s_temp[0x00000117] = "INCOMING_ABILITY_DAMAGE_ADJUSTMENT";
                s_temp[0x00000118] = "INCOMING_COMBAT_DAMAGE_ADJUSTMENT";
                s_temp[0x00000119] = "OUTGOING_ABILITY_DAMAGE_ADJUSTMENT";
                s_temp[0x0000011A] = "OUTGOING_COMBAT_DAMAGE_ADJUSTMENT";
                s_temp[0x0000011B] = "OUTGOING_ABILITY_DAMAGE_MULTIPLIER";
                s_temp[0x0000011C] = "OUTGOING_ABILITY_DAMAGE_CAP";
                s_temp[0x0000011D] = "INCOMING_ABILITY_DAMAGE_MULTIPLIER";
                s_temp[0x0000011E] = "INCOMING_ABILITY_DAMAGE_CAP";
                s_temp[0x0000011F] = "OUTGOING_COMBAT_DAMAGE_MULTIPLIER";
                s_temp[0x00000120] = "OUTGOING_COMBAT_DAMAGE_CAP";
                s_temp[0x00000121] = "INCOMING_COMBAT_DAMAGE_MULTIPLIER";
                s_temp[0x00000122] = "INCOMING_COMBAT_DAMAGE_CAP";
                s_temp[0x00000126] = "IS_MORPHED";
                s_temp[0x00000127] = "TEMP_RESOURCES";
                s_temp[0x00000128] = "OVERLOAD_OWED";
                s_temp[0x00000129] = "NUM_ATTACKS_THIS_TURN";
                s_temp[0x0000012E] = "NEXT_ALLY_BUFF";
                s_temp[0x0000012F] = "MAGNET";
                s_temp[0x00000130] = "FIRST_CARD_PLAYED_THIS_TURN";
                s_temp[0x000000BA] = "CARD_ID";
                s_temp[0x00000137] = "CANT_BE_TARGETED_BY_ABILITIES";
                s_temp[0x00000138] = "SHOULDEXITCOMBAT";
                s_temp[0x0000013C] = "PARENT_CARD";
                s_temp[0x0000013D] = "NUM_MINIONS_PLAYED_THIS_TURN";
                s_temp[0x0000014C] = "CANT_BE_TARGETED_BY_HERO_POWERS";
                s_temp[0x0000017B] = "CANT_BE_TARGETED_BY_BATTLECRIES";
                s_temp[0x0000019D] = "CANNOT_ATTACK_HEROES";
                s_temp[0x000000DC] = "COMBO";
                s_temp[0x00000072] = "ELITE";
                s_temp[0x000000B7] = "CARD_SET";
                s_temp[0x000000C9] = "FACTION";
                s_temp[0x000000CB] = "RARITY";
                s_temp[0x000000C7] = "CLASS";
                s_temp[0x00000006] = "MISSION_EVENT";
                s_temp[0x000000D0] = "FREEZE";
                s_temp[0x000000D7] = "OVERLOAD";
                s_temp[0x00000153] = "SILENCE";
                s_temp[0x00000154] = "COUNTER";
                s_temp[0x00000156] = "ARTISTNAME";
                s_temp[0x0000015F] = "FLAVORTEXT";
                s_temp[0x00000160] = "FORCED_PLAY";
                s_temp[0x00000161] = "LOW_HEALTH_THRESHOLD";
                s_temp[0x00000164] = "SPELLPOWER_DOUBLE";
                s_temp[0x00000165] = "HEALING_DOUBLE";
                s_temp[0x00000166] = "NUM_OPTIONS_PLAYED_THIS_TURN";
                s_temp[0x00000167] = "NUM_OPTIONS";
                s_temp[0x00000168] = "TO_BE_DESTROYED";
                s_temp[0x00000151] = "HEALTH_MINIMUM";
                s_temp[0x0000016A] = "AURA";
                s_temp[0x0000016B] = "POISONOUS";
                s_temp[0x0000016C] = "HOW_TO_EARN";
                s_temp[0x0000016D] = "HOW_TO_EARN_GOLDEN";
                s_temp[0x0000016E] = "HERO_POWER_DOUBLE";
                s_temp[0x0000016F] = "AI_MUST_PLAY";
                s_temp[0x00000170] = "NUM_MINIONS_PLAYER_KILLED_THIS_TURN";
                s_temp[0x00000171] = "NUM_MINIONS_KILLED_THIS_TURN";
                s_temp[0x00000172] = "AFFECTED_BY_SPELL_POWER";
                s_temp[0x00000173] = "EXTRA_DEATHRATTLES";
                s_temp[0x00000174] = "START_WITH_1_HEALTH";
                s_temp[0x00000175] = "IMMUNE_WHILE_ATTACKING";
                s_temp[0x00000176] = "MULTIPLY_HERO_DAMAGE";
                s_temp[0x00000177] = "MULTIPLY_BUFF_VALUE";
                s_temp[0x00000178] = "CUSTOM_KEYWORD_EFFECT";
                s_temp[0x00000179] = "TOPDECK";
                s_temp[0x0000017C] = "SHOWN_HERO_POWER";
                s_temp[0x0000017E] = "DEATHRATTLE_RETURN_ZONE";
                s_temp[0x00000181] = "DISPLAYED_CREATOR";
                s_temp[0x00000182] = "POWERED_UP";
                s_temp[0x00000184] = "SPARE_PART";
                s_temp[0x00000185] = "FORGETFUL";
                s_temp[0x00000186] = "CAN_SUMMON_MAXPLUSONE_MINION";
                s_temp[0x00000187] = "OBFUSCATED";
                s_temp[0x00000188] = "BURNING";
                s_temp[0x00000189] = "OVERLOAD_LOCKED";
                s_temp[0x0000018A] = "NUM_TIMES_HERO_POWER_USED_THIS_GAME";
                s_temp[0x0000018B] = "CURRENT_HEROPOWER_DAMAGE_BONUS";
                s_temp[0x0000018C] = "HEROPOWER_DAMAGE";
                s_temp[0x0000018D] = "LAST_CARD_PLAYED";
                s_temp[0x0000018E] = "NUM_FRIENDLY_MINIONS_THAT_DIED_THIS_TURN";
                s_temp[0x0000018F] = "NUM_CARDS_DRAWN_THIS_TURN";
                s_temp[0x00000190] = "AI_ONE_SHOT_KILL";
                s_temp[0x00000191] = "EVIL_GLOW";
                s_temp[0x00000192] = "HIDE_COST";
                s_temp[0x00000193] = "INSPIRE";
                s_temp[0x00000194] = "RECEIVES_DOUBLE_SPELLDAMAGE_BONUS";
                s_temp[0x0000019A] = "REVEALED";
                s_temp[0x0000019C] = "NUM_FRIENDLY_MINIONS_THAT_DIED_THIS_GAME";
                s_temp[0x0000019E] = "LOCK_AND_LOAD";
                s_temp[0x0000019F] = "TREASURE";
                s_temp[0x000001A0] = "SHADOWFORM";
                s_temp[0x00000036] = "HISTORY_PROXY";
                s_temp[0x000001A1] = "NUM_FRIENDLY_MINIONS_THAT_ATTACKED_THIS_TURN";
                s_temp[0x00000037] = "COPY_DEATHRATTLE";
                s_temp[0x00000038] = "COPY_DEATHRATTLE_INDEX";
                s_temp[0x000001A2] = "NUM_RESOURCES_SPENT_THIS_GAME";
                s_temp[0x000001A3] = "CHOOSE_BOTH";
                s_temp[0x000001A4] = "ELECTRIC_CHARGE_LEVEL";
                s_temp[0x000001A5] = "HEAVILY_ARMORED";
                s_temp[0x000001A6] = "DONT_SHOW_IMMUNE";
                s_temp[0x000001AB] = "HISTORY_PROXY_NO_BIG_CARD";
            }
        }
    }
}
