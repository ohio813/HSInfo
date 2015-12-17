
/*
 * File: Loader.cs
 * Notes:
 */

using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace HSInfo {
    namespace Server {
        public class Loader {
            static void OnTurnChange(int oldTurn, int newTurn, object userData) {
                GameState gs = GameState.Get();

                if (gs.IsFriendlySidePlayerTurn() == false)
                    return;

                //CaptureState(gs);
                //s_stream.flush();
            }
            static void OnCurrentPlayerChanged(Player player, object userData) {
                DialogManager.Get().ShowMessageOfTheDay(player.GetName());
            }
            static void OnBoardEvent(float f) {
                DialogManager.Get().ShowMessageOfTheDay("board event");
            }
            static void doRegisterGameStateListeners(GameState gs) {
                gs.RegisterCreateGameListener(s_createGameCB);

                DialogManager.Get().ShowMessageOfTheDay(gs.GetGameEntity().GetType().ToString());

                //gs.RemoveEntity(gs.GetGameEntity());

                var prop = gs.GetType().GetField("m_gameEntity", System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.Instance);

                var sge = new StandardGameEntityExt((StandardGameEntity)gs.GetGameEntity());
                prop.SetValue(gs, sge);
                //gs.AddEntity(sge);

                DialogManager.Get().ShowMessageOfTheDay(gs.GetGameEntity().GetType().ToString());

                gs.RegisterTurnChangedListener(s_turnChangedCallback);

                BoardEvents.Get().RegisterFriendlyMinionSpawnEvent(OnBoardEvent);
                BoardEvents.Get().RegisterFriendlyMinionDeathEvent(OnBoardEvent);
            }
            static void OnGameStateInit(GameState gs, object userData) {
                doRegisterGameStateListeners(gs);
            }
            static void OnCreateGameCallback(GameState.CreateGamePhase phase, object userData) {
                DialogManager.Get().ShowMessageOfTheDay("GameCreated");
            }
            static void OnSceneLoaded(SceneMgr.Mode mode, Scene scene, object userData) {
                string msg = "Scene Loaded: " + scene.name;
                DialogManager.Get().ShowMessageOfTheDay(msg);
            }
            public static void Load() {
                DialogManager.Get().ShowMessageOfTheDay("Client Connected!");

                s_gameStateInitCallback = new GameState.GameStateInitializedCallback(OnGameStateInit);
                GameState.RegisterGameStateInitializedListener(s_gameStateInitCallback);

                s_turnChangedCallback = new GameState.TurnChangedCallback(OnTurnChange);
                s_createGameCB = new GameState.CreateGameCallback(OnCreateGameCallback);

                if (GameState.Get() != null) {
                    doRegisterGameStateListeners(GameState.Get());
                }

                SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
            }
            public static void Unload() {
                GameState.Get().UnregisterTurnChangedListener(s_turnChangedCallback);
            }
            /* --- Static Fields --- */
            static GameState.GameStateInitializedCallback s_gameStateInitCallback;
            static GameState.TurnChangedCallback s_turnChangedCallback;
            static GameState.CreateGameCallback s_createGameCB;
        }
    }
}