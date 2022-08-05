using GameModes.LobbyMode.LobbyPlayers;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes.Lobby
{
    /// <summary>
    /// Force a LobbyPlayer to wear your outfit.
    /// </summary>
    internal class ForceOutfit : SimpleLobbyPlayerCheat
    {
        internal override string Name => "Force equip an outfit";

        protected override void OnExecute(LobbyPlayer player)
        {
            LobbyPlayer localPlayer = LobbyPlayers.localPlayer;
            
            var explorer = localPlayer.explorerClassLoadout;
            var neighbour = localPlayer.neighborClassLoadout;

            var playerInfo = player.playerInfo;

            CheatUtils.ChangePlayerInfo(player, playerInfo, explorer, neighbour);
        }
    }
}
