using GameModes.GameplayMode.Actors.Shared;
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
    /// Crash a player in-game.
    /// </summary>
    internal class Crash : SimplePlayerCheat
    {
        internal override string Name => "Crash Player";

        protected override void OnExecute(Player player)
        {
            SendPlayerMessage(ActorTeleportPositionMessage.Method_Public_Static_ActorTeleportPositionMessage_Vector3_0(new Vector3(float.MaxValue, float.MaxValue, float.MaxValue)), player);
        }
    }
}
