using GameModes.GameplayMode.Interactables.SceneObjects.Rocket;
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
    /// Cause the rocket to launch.
    /// </summary>
    internal class Rocket : SimpleCheat
    {
        internal override string Name => "Launch rocket";

        protected override void OnExecute()
        {
            UnityEngine.Object.FindObjectOfType<RocketLaunchButton>()?.Method_Private_Void_Actor_PDM_0(Players.localPlayer.prop_Actor_0);
        }
    }
}
