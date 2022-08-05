using Configuration.Configs;
using Configuration.Configs.Debug;
using ExitGames.Client.Photon;
using MelonLoader;
using Photon.Pun;
using SecretNeighbour.Cheats.Features;
using SecretNeighbour.Configs;
using SecretNeighbour.Menu;
using SecretNeighbour.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SecretNeighbour.Cheats.Gamemodes.Lobby
{
    internal class LobbyCheatController : IGameMode
    {
        internal LobbyCheatController()
        {
            instance = this;
        }


        void IGameMode.Start()
        {
            players = new LobbyPlayerController();
        }

        void IGameMode.End()
        {
            
        }

        void IGameMode.Update()
        {
            if (!Input.anyKeyDown || !Input.anyKey)
                return;

            if (Input.GetKeyDown(KeyCode.Insert))
                LobbyGUI.drawMenu = !LobbyGUI.drawMenu;
        }

        void IGameMode.FixedUpdate()
        {
            if (CheatConfig.current.forceHost)
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
        }

        void IGameMode.OnGUI()
        {
            gui.Draw();
        }

        internal static LobbyCheatController instance;

        internal LobbyPlayerController players;

        internal LobbyGUI gui = new LobbyGUI();

        EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique IGameMode.Gamemode { get; } = EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.LOBBY;

        #region Cheats
        public AntiKick antiKick = ContinuousCheat.AddFeature<AntiKick>(false);
        public CrashLobby crashLobby = ContinuousCheat.AddFeature<CrashLobby>(false);
        public SmallNeighbour smallNeighbour = SimpleLobbyPlayerCheat.AddFeature<SmallNeighbour>();
        public ForceOutfit forceOutfit = SimpleLobbyPlayerCheat.AddFeature<ForceOutfit>();
        #endregion
    }
}
