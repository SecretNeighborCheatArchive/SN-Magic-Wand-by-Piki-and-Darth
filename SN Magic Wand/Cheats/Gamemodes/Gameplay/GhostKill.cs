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
    /// Kill somebody for every other player except them.
    /// </summary>
    internal class GhostKill : SimplePlayerCheat
    {
        internal override string Name => "Ghost Kill";

        protected override void OnExecute(Player player)
        {
            CheatUtils.GhostKill(player);
        }
    }
}
