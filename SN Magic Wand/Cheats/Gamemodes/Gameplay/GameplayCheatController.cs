using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SecretNeighbour.Cheats.Features;
using MelonLoader;
using SecretNeighbour.Cheats.Gamemodes.Gameplay.ESP;
using GameModes.LobbyMode.LobbyPlayers;
using System.Reflection;
using GameModes.GameplayMode.Players;
using SecretNeighbour.Cheats.Hooks;
using System.Diagnostics;
using System.Collections;
using SecretNeighbour.Menu;
using SecretNeighbour.Configs;
using GameModes.GameplayMode.Actors.Shared.Configuration;
using UnhollowerRuntimeLib;
using GameModes.GameplayMode.Actors;
using GameModes.GameplayMode.Interactables.InventoryItems;
using HoloNetwork.Players;
using GameModes.GameplayMode;
using Configuration.Configs;
using Configuration.Configs.Debug;
using Object = UnityEngine.Object;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class GameplayCheatController : IGameMode
    {

        internal GameplayCheatController()
        {
            instance = this;
        }

        void IGameMode.Start()
        {
            CheatConfig.current.antiBuff = false;

            players = new PlayersController();

            cam = Camera.main;

            gui.Start();
        }

        void IGameMode.End()
        {
            Time.timeScale = 1f;
            CheatConfig.current.antiBuff = false;
        }

        void IGameMode.Update()
        {
            aimbot.Enabled = CheatConfig.current.aimbot;

            players.Update();

            if (!Input.anyKeyDown || !Input.anyKey)
                return;

            /*if (Input.GetKeyDown(KeyCode.Z))
            {
                RifleInventoryItem rifle = Objects.Items.rifles[0];

                if (rifle)
                    players.localPlayer.field_Private_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0
                    (
                        InventoryTakeItemMessage.Method_Public_Static_InventoryTakeItemMessage_InventoryItem_Int32_PDM_0
                        (
                            rifle, 1
                        ),
                        EnumPublicSealedvaOtAlSe5vSeUnique.All
                    );
            }*/

            if (Input.GetKeyDown(KeyCode.Insert))
                GameplayGUI.drawMenu = !GameplayGUI.drawMenu;
            if (Input.GetKeyDown(Controls.current.noclip)) 
                noclip.Toggle();
            if (Input.GetKeyDown(KeyCode.F1))
                speedhack.Toggle();
            if (Input.GetKeyDown(KeyCode.F2))
                fullBright.Toggle();
            if (Input.GetKeyDown(KeyCode.F3))
                godmode.Toggle();
        }

        void IGameMode.FixedUpdate()
        {
            
        }

        void IGameMode.OnGUI()
        {
            gui.Draw();
        }

        internal Camera cam;
        internal static GameplayCheatController instance;
        internal GameplayGUI gui = new GameplayGUI();

        internal PlayersController players;

        EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique IGameMode.Gamemode { get; } = EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        // Features
        #region General
        internal Noclip noclip = ContinuousCheat.AddFeature<Noclip>(false);
        internal Speedhack speedhack = ContinuousCheat.AddFeature<Speedhack>(false);
        internal Fullbright fullBright = ContinuousCheat.AddFeature<Fullbright>(false);
        internal Aimbot aimbot = ContinuousCheat.AddFeature<Aimbot>(false);
        internal Crash crash = SimplePlayerCheat.AddFeature<Crash>();
        internal Teleport teleport = SimplePlayerCheat.AddFeature<Teleport>();
        internal Godmode godmode = ContinuousCheat.AddFeature<Godmode>(false);
        internal Rocket rocket = SimpleCheat.AddFeature<Rocket>();
        internal QuestWin questWin = SimpleCheat.AddFeature<QuestWin>();
        internal NeighbourWin neighbourWin = SimpleCheat.AddFeature<NeighbourWin>();
        internal KidsWin kidsWin = SimpleCheat.AddFeature<KidsWin>();
        //public UnrestrictedMovement movement = ContinuousCheat.AddFeature<UnrestrictedMovement>(true);
        #endregion

        #region Hooks
        internal AntiBuff antiBuff;
        #endregion

        #region ESP
        internal RifleESP rifleESP = ContinuousCheat.AddFeature<RifleESP>(false);
        internal RifleChams rifleChams = ContinuousCheat.AddFeature<RifleChams>(false);
        internal KeyNameESP keyNameESP = ContinuousCheat.AddFeature<KeyNameESP>(false);
        internal PlayerNameESP plrNameESP = ContinuousPlayerCheat.AddFeature<PlayerNameESP>(true);
        internal Chams chams = ContinuousPlayerCheat.AddFeature<Chams>(true);
        internal PlayerBoxESP playerBoxESP = ContinuousCheat.AddFeature<PlayerBoxESP>(false);
        internal ThreeDPlayerBoxESP boxESP = ContinuousPlayerCheat.AddFeature<ThreeDPlayerBoxESP>(false);
        internal Crosshair crosshair = ContinuousCheat.AddFeature<Crosshair>(false);
        internal KeyChams keyChams = ContinuousCheat.AddFeature<KeyChams>(false);
        internal KeyBoxESP keyBox = ContinuousCheat.AddFeature<KeyBoxESP>(false);
        internal GodmodeESP godmodeESP = ContinuousPlayerCheat.AddFeature<GodmodeESP>(false);
        //public ClearVision clearVision = ContinuousCheat.AddFeature<ClearVision>(true);
        internal BasementDoor basementDoor = ContinuousCheat.AddFeature<BasementDoor>(false);
        //public BoneESP boneESP = ContinuousCheat.AddFeature<BoneESP>(true);
        //public PlayerHealth playerHealth = ContinuousPlayerCheat.AddFeature<PlayerHealth>(true);
        #endregion
    }
}
