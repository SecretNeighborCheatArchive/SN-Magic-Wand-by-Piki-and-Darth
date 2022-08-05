using GameModes.GameplayMode.Players;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    /// <summary>
    /// Kill a player.
    /// </summary>
    internal class Kill : SimplePlayerCheat
    {
        internal override string Name => "Kill a player";

        protected override void OnExecute(Player player)
        {
            SendPlayerMessage(DecideDeathMessage.Method_Public_Static_DecideDeathMessage_EnumPublicSealedvaNEKNBARE5vUnique_0(EnumPublicSealedvaNEKNBARE5vUnique.NEIGHBOR_KILL));
        }
    }
}
