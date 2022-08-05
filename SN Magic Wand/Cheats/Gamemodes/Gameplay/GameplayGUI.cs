using GameModes.GameplayMode;
using GameModes.GameplayMode.Players;
using HoloNetwork.Messaging.Implementations;
using HoloNetwork.Players;
using SecretNeighbour.Cheats.Hooks;
using SecretNeighbour.Configs;
using SecretNeighbour.Menu;
using SecretNeighbour.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    class GameplayGUI : GUITypeBase
    {
        //private bool hasInitialisedItems;

        internal void Start()
        {
        //    if (ObjectPublicOb287259405257206727Unique.prop_ObjectPublicOb287259405257206727Unique_0 == null ||
        //        ObjectPublicOb287259405257206727Unique.prop_ObjectPublicOb287259405257206727Unique_0.field_Public_ObjectPublicLi1GaDi2HoUIHoInUIUnique_0 == null)
        //        return;

        //    var list = ObjectPublicOb287259405257206727Unique.prop_ObjectPublicOb287259405257206727Unique_0.field_Public_ObjectPublicLi1GaDi2HoUIHoInUIUnique_0.field_Private_List_1_GameObject_0;

        //    if (list.Count > 0)
        //    {
        //        hasInitialisedItems = true;

        //        foreach (GameObject go in ObjectPublicOb287259405257206727Unique.prop_ObjectPublicOb287259405257206727Unique_0.field_Public_ObjectPublicLi1GaDi2HoUIHoInUIUnique_0.field_Private_List_1_GameObject_0)
        //            items.Add(new SpawnButton(go));
        //    }

        //    return;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DrawGUI(0)]
        void Menus()
        {
        //    if (!hasInitialisedItems)
        //        Start();

            if (drawMenu)
            {
                wRectGen = GUILayout.Window(wIDGen, wRectGen, (GUI.WindowFunction)GeneralMenu, "General", default);
                wRectESP = GUILayout.Window(wIDESP, wRectESP, (GUI.WindowFunction)ESPMenu, "ESP", default);
                wRectPlayers = GUILayout.Window(wIDPlayers, wRectPlayers, (GUI.WindowFunction)PlayerMenu, "Players", default);
                //wRectItemSpawner = GUILayout.Window(wIDItemSpawner, wRectItemSpawner, (GUI.WindowFunction)ItemSpawnerMenu, "Item Spawner", default);
                wRectProtection = GUILayout.Window(wIDProtection, wRectProtection, (GUI.WindowFunction)ProtectionMenu, "Protection", default);

                if (Event.current.type == EventType.Repaint)
                    ESPUtils.DrawString1(new Vector2(5f, 5f), "SN Magic Wand", titleCol, false, 24, FontStyle.Italic, 3);
            }
        }


        private void GeneralMenu(int wID)
        {
            wScrollPosGeneral = GUILayout.BeginScrollView(wScrollPosGeneral, new GUILayoutOption[2] { GUILayout.Height(300f), GUILayout.ExpandWidth(true) });
            {
                GUILayout.BeginVertical("Keybinds", GUI.skin.box, default);
                {
                    GUILayout.Space(20f);

                    GUILayout.Label("<color=yellow>[INSERT] Open/Close menu</color>", default);
                    GUILayout.Label(MakeEnable("[F1] Speedhack ", ins.speedhack.Enabled), default);
                    GUILayout.Label(MakeEnable("[F2] Fullbright ", ins.fullBright.Enabled), default);
                    GUILayout.Label(MakeEnable("[F3] Godmode ", ins.godmode.Enabled), default);
                    GUILayout.Label(MakeEnable("[C] Noclip ", ins.noclip.Enabled), default);
                }
                GUILayout.EndVertical();

                GUILayout.Space(5f);

                GUILayout.BeginVertical("Aimbot", GUI.skin.box, default);
                {
                    GUILayout.Space(20f);

                    CheatConfig.current.aimbot = GUILayout.Toggle(CheatConfig.current.aimbot, "Aimbot", default);
                    CheatConfig.current.silentAim = GUILayout.Toggle(CheatConfig.current.silentAim, "Silent", default);
                    CheatConfig.current.drawTracers = GUILayout.Toggle(CheatConfig.current.drawTracers, "Tracers", default);
                    CheatConfig.current.aimXhair = GUILayout.Toggle(CheatConfig.current.aimXhair, "Aim crosshair", default);
                    Generic.infRifleAmmo = GUILayout.Toggle(Generic.infRifleAmmo, "Infinite ammo", default);
                }
                GUILayout.EndVertical();

                GUILayout.Space(5f);

                GUILayout.BeginVertical("Misc", GUI.skin.box, default);
                {
                    GUILayout.Space(20f);

                    GUILayout.BeginHorizontal(default);
                    {
                        GUILayout.Label($"TimeScale: {Time.timeScale:0.00}", default);

                        Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale, 0.5f, 3f, default);

                        if (GUILayout.Button("Normal", default))
                            Time.timeScale = 1f;
                    }
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Launch Rocket", default))
                        ins.rocket.Execute();

                    if (GUILayout.Button("TP All here", default))
                    {
                        ins.teleport.tpToMe = true;
                        ins.teleport.ExecuteOnAll();
                    }

                    GUILayout.BeginHorizontal(default);
                    {
                        if (GUILayout.Button("Quest win", default))
                            ins.questWin.Execute();
                        if (GUILayout.Button("Kids win", default))
                            ins.kidsWin.Execute();
                        if (GUILayout.Button("Neighbour win", default))
                            ins.neighbourWin.Execute();
                    }
                    GUILayout.EndHorizontal();

                    CheatConfig.current.noSkillCooldowns = GUILayout.Toggle(CheatConfig.current.noSkillCooldowns, "No skill cooldowns", default);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndScrollView();

            GUI.DragWindow();
        }


        private void ESPMenu(int wID)
        {
            GUILayout.BeginVertical("Options", GUI.skin.box, new GUILayoutOption[1] { GUILayout.ExpandWidth(true) });
            {
                GUILayout.Space(20f);

                GUILayout.BeginHorizontal(default);
                {
                    ins.playerBoxESP.Enabled = GUILayout.Toggle(ins.playerBoxESP.Enabled, "Player box", default);
                    CheatConfig.current.playerNameESP = GUILayout.Toggle(CheatConfig.current.playerNameESP, "Player name", default);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                {
                    ins.keyNameESP.Enabled = GUILayout.Toggle(ins.keyNameESP.Enabled, "Key", default);
                    ins.rifleESP.Enabled = GUILayout.Toggle(ins.rifleESP.Enabled, "Rifle", default);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                {
                    ins.basementDoor.Enabled = GUILayout.Toggle(ins.basementDoor.Enabled, "Basement", default);
                    ins.crosshair.Enabled = GUILayout.Toggle(ins.crosshair.Enabled, "Crosshair", default);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                {
                    CheatConfig.current.godmodeESP = GUILayout.Toggle(CheatConfig.current.godmodeESP, "Godmode ESP", default);
                    ins.keyChams.Enabled = GUILayout.Toggle(ins.keyChams.Enabled, "Key chams", default);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                {
                    ins.rifleChams.Enabled = GUILayout.Toggle(ins.rifleChams.Enabled, "Rifle chams", default);
                    ins.aimbot.drawFov = GUILayout.Toggle(ins.aimbot.drawFov, "Draw FOV", default);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                {
                    CheatConfig.current.chams = GUILayout.Toggle(CheatConfig.current.chams, "Chams", default);
                    if (GUILayout.Button("Glow", default))
                    {
                        ins.chams.outline = false;
                        ins.chams.curChamType = Chams.ChamsType.XRAY;
                    }
                    if (GUILayout.Button("Flat", default))
                    {
                        ins.chams.outline = false;
                        ins.chams.curChamType = Chams.ChamsType.FLAT;
                    }
                    if (GUILayout.Button("Outline", default))
                        ins.chams.outline = true;
                }
                GUILayout.EndHorizontal();
                    
                if (ins.cam != null)
                    ins.cam.useOcclusionCulling = GUILayout.Toggle(ins.cam.useOcclusionCulling, "Occlusion culling", default);
            }
            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        private void PlayerMenu(int wID)
        {
            GUILayout.Label($"Selected player: {((selectedPlayer == null) ? "NONE" : selectedPlayer.prop_PlayerInfo_0.displayName)}", default);

            GUILayout.BeginHorizontal(GUI.skin.box, default);
            foreach (Player player in ins.players.AllPlayers)
            {
                string nameText = player.prop_PlayerInfo_0.displayName;

                if (selectedPlayer == player)
                    nameText = $"<color=yellow>{nameText}</color>";
                if (GUILayout.Button(nameText, default))
                    selectedPlayer = player;
            }
                
            GUILayout.EndHorizontal();

            GUILayout.Space(15f);

            if (selectedPlayer != null)
            {
                GUILayout.BeginVertical("Options", GUI.skin.box, new GUILayoutOption[1] { GUILayout.ExpandWidth(false) });
                GUILayout.Space(30f);
                GUILayout.BeginHorizontal(default);
                if (GUILayout.Button("Ghost Kill", new GUILayoutOption[1] { GUILayout.ExpandWidth(true) }))
                    CheatUtils.GhostKill(selectedPlayer);
                if (GUILayout.Button("Kill", new GUILayoutOption[1] { GUILayout.ExpandWidth(true) }))
                    CheatUtils.Kill(selectedPlayer);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                if (GUILayout.Button("Give godmode", new GUILayoutOption[1] { GUILayout.ExpandWidth(true) }))
                    selectedPlayer.Buff(EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.INVINCIBLE);
                if (GUILayout.Button("Take godmode", new GUILayoutOption[1] { GUILayout.ExpandWidth(true) }))
                    selectedPlayer.Debuff(EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.INVINCIBLE);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(default);
                if (GUILayout.Button("TP to", new GUILayoutOption[1] { GUILayout.ExpandWidth(true) }))
                {
                    ins.teleport.pos = selectedPlayer.prop_Actor_0.transform.position;
                    ins.teleport.ExecuteOnLocalPlayer();
                }
                if (GUILayout.Button("TP here", new GUILayoutOption[1] { GUILayout.ExpandWidth(true) }))
                {
                    ins.teleport.pos = ins.players.localPlayer.prop_Actor_0.transform.position;
                    ins.teleport.Execute(selectedPlayer);
                }
                GUILayout.EndHorizontal();
                /*if (GUILayout.Button("Crash", default))
                    ins.crash.Execute(selectedPlayer);
                if (GUILayout.Button("Test", default))
                {
                    // Respawn our player for other people.
                    foreach (HoloNetPlayer holoNetPlayer in HoloNetPlayer.prop_List_1_HoloNetPlayer_0)
                        selectedPlayer.field_Private_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_HoloNetPlayer_0(InitializePlayerMessage.Method_Public_Static_InitializePlayerMessage_PlayerInfo_ActorClassLoadout_ActorClassLoadout_0(selectedPlayer.prop_PlayerInfo_0, selectedPlayer.prop_ActorClassLoadout_0, selectedPlayer.prop_ActorClassLoadout_1), holoNetPlayer);
                }*/
                GUILayout.EndVertical();
            }

            GUI.DragWindow();
        }

        //private void ItemSpawnerMenu(int wID)
        //{
        //    GUILayout.BeginVertical("Items", GUI.skin.box, new GUILayoutOption[1] { GUILayout.ExpandWidth(true) });
        //    {
        //        GUILayout.Space(20f);
                
        //        wScrollPosItemSpawner = GUILayout.BeginScrollView(wScrollPosItemSpawner, new GUILayoutOption[2] { GUILayout.Height(400f), GUILayout.ExpandWidth(true) });
        //        {
        //            for (int i = 0; i < items.Count; i++)
        //                if (items[i] != null)
        //                    if (GUILayout.Button(items[i].name, default))
        //                    {
        //                        items[i].Spawn();

        //                        string nameLower = items[i].name.ToLower();

        //                        if (nameLower.Contains("rifle"))
        //                            ins.rifleChams.appliedChams = false;
        //                        if (nameLower.Contains("key") || nameLower.Contains("card"))
        //                            ins.keyChams.appliedChams = false;
        //                    }        
        //        }
        //        GUILayout.EndScrollView();
        //    }
        //    GUILayout.EndVertical();

        //    GUI.DragWindow();
        //}

        private void ProtectionMenu(int wID)
        {
            GUILayout.BeginVertical("Options", GUI.skin.box, new GUILayoutOption[1] { GUILayout.ExpandWidth(true) });
            {
                GUILayout.Space(20f);

                GUILayout.Label("<color=yellow>Temporarly disabled</color>", default);
                //CheatConfig.current.antiKill = GUILayout.Toggle(CheatConfig.current.antiKill, "Anti Kill", default);
                //CheatConfig.current.antiTP = GUILayout.Toggle(CheatConfig.current.antiTP, "Anti TP", default);

                /*GUILayout.Label("<color=red>WARNING:</color>\nOnly use Anti Buff after you've spawned in!", default);

                if (GUILayout.Button(MakeEnable("Anti Buff ", CheatConfig.current.antiBuff), default))
                {
                    CheatConfig.current.antiBuff = !CheatConfig.current.antiBuff;

                    ins.antiBuff.Hook();
                }*/
            }
            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        //private void Spawn(int prefabID)
        //{
        //    Transform cam = GameplayCheatController.instance.cam.transform;
        //    Ray ray = new Ray(cam.position, cam.forward);
        //    RaycastHit hit;
        //    bool isHit = Physics.Raycast(ray, out hit, 3);
        //    Vector3 spawnPos = isHit ? hit.point : cam.position + cam.forward * 3;
        //    ObjectPublicDoBoObBoUnique.Method_Public_Static_Void_HoloNetGlobalMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0
        //    (
        //        SpawnNetObjectMessage.Method_Public_Static_SpawnNetObjectMessage_HoloNetObjectId_Int32_Vector3_Quaternion_0
        //        (
        //            ObjectPublicOb287259405257206727Unique.prop_ObjectPublicOb287259405257206727Unique_0.field_Public_ObjectPublicLi1GaDi2HoUIHoInUIUnique_0.Method_Private_HoloNetObjectId_0(),
        //            prefabID, spawnPos, default
        //        ),
        //        EnumPublicSealedvaOtAlSe5vSeUnique.All
        //    );
        //}

        private string MakeEnable(string text, bool state)
        {
            return string.Format("{0}{1}", text, state ? "<color=#7CFC00>ON</color>" : "<color=#EB2E2E>OFF</color>");
        }

        private static GameplayCheatController ins => GameplayCheatController.instance;

        private List<SpawnButton> items = new List<SpawnButton>();

        public static bool drawMenu = true;

        private readonly Color32 titleCol = new Color32(30, 144, 255, 255);

        private Player selectedPlayer = new Player();

        private Vector2 wScrollPosGeneral;
        private int wIDGen = MenuUtils.GenerateWindowIDUnique();
        private Rect wRectGen = new Rect(5f, 37f, 290f, 50f);

        private int wIDESP = MenuUtils.GenerateWindowIDUnique();
        private Rect wRectESP = new Rect(305f, 5f, 50f, 50f);

        private Vector2 wScrollPosItemSpawner;
        private int wIDItemSpawner = MenuUtils.GenerateWindowIDUnique();
        private Rect wRectItemSpawner = new Rect(535f, 5f, 300f, 400f);

        private int wIDPlayers = MenuUtils.GenerateWindowIDUnique();
        private Rect wRectPlayers = new Rect(1040f, 5f, 200f, 100f);

        private int wIDProtection = MenuUtils.GenerateWindowIDUnique();
        private Rect wRectProtection = new Rect(835f, 5f, 200f, 50f);
    }
}