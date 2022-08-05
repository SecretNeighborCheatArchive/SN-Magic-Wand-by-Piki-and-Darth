using GameModes.GameplayMode.Players;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    /// <summary>
    /// Teleport a player.
    /// </summary>
    internal class Teleport : SimplePlayerCheat
    {
        internal override string Name => "Teleport a player";

        internal Vector3 pos = Vector3.zero;
        internal bool tpToMe;

        protected override void OnExecute(Player player)
        {
            MelonLoader.MelonLogger.LogWarning("TP attempt");
            if (pos == Vector3.zero || tpToMe)
                pos = Players.localPlayer.prop_Actor_0.transform.position;


            if (tpToMe && player == Players.localPlayer)
                return;

            tpToMe = false;

            MelonLoader.MelonLogger.LogWarning("TP executing");
            player.Teleport(pos);
            MelonLoader.MelonLogger.LogWarning("TP done");
        }
    }
}
