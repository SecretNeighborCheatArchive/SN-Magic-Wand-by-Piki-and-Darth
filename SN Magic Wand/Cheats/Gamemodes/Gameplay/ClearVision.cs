using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class ClearVision : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Clear Vision";

        private PostProcessLayer pp;

        protected override void OnStart()
        {
            pp = GameObject.FindObjectOfType<PostProcessLayer>();
        }

        protected override void OnEnabledToggle()
        {
            pp.enabled = !Enabled;
        }
    }
}       
