using GameModes.GameplayMode.Actors;
using MelonLoader;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class Speedhack : ContinuousCheat
    {
        internal override string Name => "Speedhack";
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal float multiplier = 1.1f;

        protected override void OnInit()
        {
            //SpeedHackDetector.Dispose();
        }

        /*protected override void OnEnabledToggle()
        {
            Time.timeScale = Enabled ? 3f : 1f;
        }*/

        protected override void OnUpdate()
        {
            if (!Input.anyKey)
                return;

            Actor curActor = Players.localPlayer.prop_Actor_0;
            Transform t = curActor.transform;

            if (Input.GetKey(KeyCode.W))
                t.position += t.forward / 10;// * multiplier;
            if (Input.GetKey(KeyCode.S))
                t.position -= t.forward / 10;// * multiplier;
            if (Input.GetKey(KeyCode.A))
                t.position -= t.right / 10;// * multiplier;
            if (Input.GetKey(KeyCode.D))
                t.position += t.right / 10;// * multiplier;
        }
    }
}
