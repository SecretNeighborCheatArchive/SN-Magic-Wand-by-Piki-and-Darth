using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class Godmode : ContinuousCheat
    {
        internal override string Name => "Godmode";
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        protected override void OnEnabledToggle()
        {
            if (!Enabled) 
                Players.localPlayer.Debuff(EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.INVINCIBLE);
        }

        protected override void OnUpdate()
        {
            if (Time.frameCount % 8 != 0) 
                return;

            Players.localPlayer.Buff(EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.INVINCIBLE);
        }
    }
}
