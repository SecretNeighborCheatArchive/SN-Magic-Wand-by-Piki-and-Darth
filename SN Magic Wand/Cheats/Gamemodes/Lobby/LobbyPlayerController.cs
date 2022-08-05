using GameModes.LobbyMode;
using GameModes.LobbyMode.LobbyPlayers;
using GameModes.LobbyMode.LobbyPlayers.Messages;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes.Lobby
{
    internal class LobbyPlayerController
    {
        internal LobbyPlayerController()
        {
            localPlayer = LobbyController.prop_LobbyController_0.players.prop_LobbyPlayer_0;
        }

        internal LobbyPlayer[] Players => LobbyController.prop_LobbyController_0.players.prop_List_1_LobbyPlayer_0.ToArray();

        internal LobbyPlayer localPlayer;


        /// <summary>
        /// Close the player's connection using Photon.
        /// </summary>
        /// <param name="target"></param>
        internal static void ForceKickPlayer(LobbyPlayer target)
        {
            var photonPlr = target.prop_HoloNetPlayer_0.Cast<HoloNetwork.NetworkProviders.Photon.PhotonHoloNetPlayer>().photonPlayer; //PhotonNetwork.PlayerList.Where(p => p.ActorNumber == target.prop_HoloNetPlayer_0.actorId).FirstOrDefault();
            if (photonPlr == null) return;
            if (photonPlr.IsMasterClient) PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            PhotonNetwork.CloseConnection(photonPlr);
        }

        /// <summary>
        /// Load the player into a game by themself and have it load forever.
        /// </summary>
        /// <param name="target"></param>
        internal static void LoadForeverExploit(LobbyPlayer target)
        {
            var explorer = target.explorerClassLoadout;
            var neighbour = target.neighborClassLoadout;

            var playerInfo = target.playerInfo;
            playerInfo.playerID = "";

            // Corrupt their player ID.
            CheatUtils.ChangePlayerInfo(target, playerInfo, explorer, neighbour);

            // Force start for them.
            CheatUtils.SendMessage(StartGameLoadingMessage.Method_Public_Static_StartGameLoadingMessage_PDM_0(), target.prop_HoloNetPlayer_0);

            // Force kick them so they cannot interact with our lobby anymore.
            ForceKickPlayer(target);
        }

        /// <summary>
        /// Change a player's name in a lobby.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="newName"></param>
        internal static void ChangeName(LobbyPlayer target, string newName)
        {
            var explorer = target.explorerClassLoadout;
            var neighbour = target.neighborClassLoadout;
            var playerInfo = target.playerInfo;

            playerInfo.displayName = newName;

            CheatUtils.ChangePlayerInfo(target, playerInfo, explorer, neighbour);
        }
    }
}
