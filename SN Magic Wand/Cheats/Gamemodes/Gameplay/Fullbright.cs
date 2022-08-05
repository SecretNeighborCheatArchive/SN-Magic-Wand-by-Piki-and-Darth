using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class Fullbright : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Fullbright";

        private Light fullBrightLight;


        protected override void OnEnabledToggle()
        {
            if (fullBrightLight == null && Enabled)
            {
                fullBrightLight = Players.localPlayer.prop_Actor_0.gameObject.AddComponent<Light>();
                fullBrightLight.enabled = true;
                fullBrightLight.type = LightType.Point;
                fullBrightLight.color = Color.white;
                fullBrightLight.range = 200f;
                fullBrightLight.intensity = 0.7f;
            }
            else if (!Enabled && fullBrightLight != null)
                UnityEngine.Object.Destroy(fullBrightLight);
        }
    }
}
