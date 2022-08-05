using GameModes.GameplayMode.Players;
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
    /// Make somebody a 'small neighbour' in the lobby.
    /// </summary>
    internal class SmallNeighbour : SimpleLobbyPlayerCheat
    {
        internal override string Name => "Small Neighbour";

        protected override void OnExecute(LobbyPlayer player)
        {
            var explorer = player.explorerClassLoadout;
            var neighbour = player.neighborClassLoadout;
            var playerInfo = player.playerInfo;

            var customizationSetup = player.neighborClassLoadout.customizationSetup;

            explorer.customizationSetup.field_Public_List_1_String_0 = customizationSetup.field_Public_List_1_String_0;

            player.ChangePlayerInfo(playerInfo, explorer, neighbour);
        }
    }
}
