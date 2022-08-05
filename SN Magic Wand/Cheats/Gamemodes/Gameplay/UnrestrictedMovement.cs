using GameModes.GameplayMode.Actors.Shared.Configuration;
using MelonLoader;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class UnrestrictedMovement : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Unrestricted movement";

        protected override void OnUpdate()
        {
            
        }
    }
}
