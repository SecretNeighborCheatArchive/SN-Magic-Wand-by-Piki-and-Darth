using BackEnd;
using ExitGames.Client.Photon;
using GameModes.LobbyMode.LobbyPlayers;
using GameModes.LobbyMode.LobbyPlayers.Messages;
using GameModes.Shared.Loadouts.Classes;
using Photon.Pun;
using Photon.Realtime;
using SecretNeighbour.Cheats.Hooks;
using SecretNeighbour.Configs;
using SecretNeighbour.Menu;
using SecretNeighbour.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Lobby
{
    class LobbyGUI : GUITypeBase
    {
        [DrawGUI(0)]
        void Menus()
        {
            if (drawMenu)
            {
                wRectGeneral = GUILayout.Window(wIDGeneral, wRectGeneral, (GUI.WindowFunction)GeneralMenu, "General", default);
                wRectPlayers = GUILayout.Window(wIDPlayers, wRectPlayers, (GUI.WindowFunction)PlayerMenu, "Players", default);
                //wRectRoom = GUILayout.Window(wIDRoom, wRectRoom, (GUI.WindowFunction)RoomMenu, "Room", default);

                if (Event.current.type == EventType.Repaint)
                    ESPUtils.DrawString1(new Vector2(5f, 5f), "SN Magic Wand", titleCol, false, 24, FontStyle.Italic, 3);
            }
        }

        private void GeneralMenu(int wID)
        {
            GUILayout.BeginVertical("Options", GUI.skin.box, default);
            {
                GUILayout.Space(20f);

                CheatConfig.current.forceHost = GUILayout.Toggle(CheatConfig.current.forceHost, "Force Host", default);
                Generic.antiKick = GUILayout.Toggle(Generic.antiKick, "Anti kick", default);

                if (GUILayout.Button("Force Start", default))
                    CheatUtils.SendMessage(StartGameLoadingMessage.Method_Public_Static_StartGameLoadingMessage_PDM_0(), EnumPublicSealedvaOtAlSe5vSeUnique.All);

                if (GUILayout.Button("Force Neighbour", default))
                {
                    var plr = ins.players.localPlayer;
                    var plrInfo = plr.playerInfo;
                    plrInfo.specialRole = true;
                    plrInfo.teamId = EnumPublicSealedvaALBEGADEEPZENECH9vUnique.NEIGHBOR;

                    CheatUtils.ChangePlayerInfo(plr, plrInfo, plr.explorerClassLoadout, plr.neighborClassLoadout);
                }
            }
            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        private void PlayerMenu(int wID)
        {
            GUILayout.Label($"Selected player name: {((selectedPlayer == null) ? "NONE" : selectedPlayer.playerInfo.displayName)}", default);
            GUILayout.Label($"ID: {((selectedPlayer == null) ? "NONE" : selectedPlayer.playerInfo.playerID)}", default);

            GUILayout.BeginHorizontal("", GUI.skin.box, default);
            foreach (LobbyPlayer player in ins.players.Players)
            {
                bool local = player == ins.players.localPlayer;

                string nameText = player.playerInfo.displayName;

                if (selectedPlayer == player)
                    nameText = $"<color={(local ? "cyan" : "yellow")}>{nameText}</color>";
                if (GUILayout.Button(nameText, new GUILayoutOption[1] { GUILayout.ExpandWidth(false) }))
                    selectedPlayer = player;
            }
            
            GUILayout.EndHorizontal();

            GUILayout.Space(15f);

            if (selectedPlayer != null)
            {
                bool isLocal = selectedPlayer == ins.players.localPlayer;

                GUILayout.BeginVertical("Options", GUI.skin.box, default);
                {
                    GUILayout.Space(15f);

                    GUILayout.BeginHorizontal(default);
                    {
                        if (GUILayout.Button("Force Kick", default))
                        {
                            var photonPlr = PhotonNetwork.PlayerList.FirstOrDefault(p => p.ActorNumber == selectedPlayer.prop_HoloNetPlayer_0.actorId);

                            if (photonPlr != null)
                                PhotonNetwork.CloseConnection(photonPlr);
                        }

                        if (GUILayout.Button("Copy Character", default))
                        {
                            PlayerInfo playerInfo = ins.players.localPlayer.playerInfo;
                            ActorClassLoadout explorer = selectedPlayer.explorerClassLoadout;
                            ActorClassLoadout neighbour = selectedPlayer.neighborClassLoadout;

                            ins.players.localPlayer.ChangePlayerInfo(playerInfo, explorer, neighbour);
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal(default);
                    {
                        if (GUILayout.Button("Copy everything", default))
                        {
                            PlayerInfo playerInfo = ins.players.localPlayer.playerInfo;
                            playerInfo.displayName = selectedPlayer.playerInfo.displayName;

                            ActorClassLoadout explorer = selectedPlayer.explorerClassLoadout;
                            ActorClassLoadout neighbour = selectedPlayer.neighborClassLoadout;

                            ins.players.localPlayer.ChangePlayerInfo(playerInfo, explorer, neighbour);
                        }

                        if (isLocal && GUILayout.Button("Toggle neighbour", default))
                        {
                            PlayerInfo plrInfo = selectedPlayer.playerInfo;
                            ActorClassLoadout explorer = selectedPlayer.explorerClassLoadout;
                            ActorClassLoadout neighbour = selectedPlayer.neighborClassLoadout;

                            selectedPlayer.ChangePlayerInfo(plrInfo, neighbour, explorer);
                        }
                    }
                    GUILayout.EndHorizontal();

                    if (isLocal)
                    {
                        GUILayout.BeginHorizontal(default);
                        {
                            if (GUILayout.Button("Make host", default))
                            {
                                var player = PhotonNetwork.PlayerListOthers.FirstOrDefault(x => x.nickName == selectedPlayer.playerInfo.displayName);

                                if (player != null)
                                    PhotonNetwork.SetMasterClient(player);
                            }

                            if (GUILayout.Button("Small Neighbour", default))
                                ins.smallNeighbour.Execute(selectedPlayer);
                        }
                        GUILayout.EndHorizontal();
                    }

                    /*if (GUILayout.Button("Wear your outfit", default))
                    {
                        PlayerInfo plrInfo = selectedPlayer.playerInfo;
                        ActorClassLoadout myExplorer = ins.players.localPlayer.explorerClassLoadout;
                        ActorClassLoadout myNeighbour = ins.players.localPlayer.neighborClassLoadout;

                        selectedPlayer.ChangePlayerInfo(plrInfo, myExplorer, myNeighbour);
                    }*/

                    if (isLocal)
                    {
                        GUILayout.BeginHorizontal(default);
                        {
                            GUILayout.Label("Name: ", default);
                            selectedPlayer.playerInfo.displayName = GUILayout.TextField(selectedPlayer.playerInfo.displayName, default);
                        }
                        GUILayout.EndHorizontal();

                        if (GUILayout.Button("Change name", default))
                        {
                            PlayerInfo plrInfo = selectedPlayer.playerInfo;
                            ActorClassLoadout explorer = selectedPlayer.explorerClassLoadout;
                            ActorClassLoadout neighbour = selectedPlayer.neighborClassLoadout;

                            selectedPlayer.ChangePlayerInfo(plrInfo, explorer, neighbour);
                        }
                    }
                }
                GUILayout.EndVertical();
            }
            GUI.DragWindow();
        }

        internal string customName = "";
        internal string customPassword = "";
        internal string customLocale = "";
        private void RoomMenu(int wID)
        {
            Room curRoom = PhotonNetwork.CurrentRoom;

            if (curRoom == null)
                return;

            GUILayout.BeginVertical("Options", GUI.skin.box, default);
            {
                GUILayout.Space(20f);

                GUILayout.BeginHorizontal(default);
                {
                    GUILayout.Label("Name:", default);
                    customName = GUILayout.TextField(customName, default);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                {
                    GUILayout.Label("Password:", default);
                    customPassword = GUILayout.TextField(customPassword, default);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                {
                    GUILayout.Label("Locale:", default);
                    customLocale = GUILayout.TextField(customLocale, default);
                }
                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Keep open forever", default))
                curRoom.EmptyRoomTtl = 9000;

            if (GUILayout.Button("Apply", default))
                CheatUtils.ChangeRoomSettings(customName, customPassword, customLocale);

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        private static LobbyCheatController ins => LobbyCheatController.instance;

        internal static bool drawMenu = true;

        private readonly Color32 titleCol = new Color32(30, 144, 255, 255);

        private LobbyPlayer selectedPlayer = new LobbyPlayer();

        private int wIDGeneral = MenuUtils.GenerateWindowIDUnique();
        private Rect wRectGeneral = new Rect(5f, 37f, 200f, 50f);

        private int wIDPlayers = MenuUtils.GenerateWindowIDUnique();
        private Rect wRectPlayers = new Rect(415f, 5f, 200f, 300f);

        private int wIDRoom = MenuUtils.GenerateWindowIDUnique();
        private Rect wRectRoom = new Rect(210f, 5f, 200f, 50f);
    }
}
