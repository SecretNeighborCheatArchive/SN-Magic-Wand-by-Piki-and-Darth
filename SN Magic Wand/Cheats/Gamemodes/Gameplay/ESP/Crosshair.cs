using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay.ESP
{
    internal class Crosshair : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Crosshair";

        internal float crosshairScale = 14f;
        internal float lineThickness = 1.75f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnGUI()
        {
            // Only paint once per frame, save FPS.
            if (Event.current.type != EventType.Repaint)
                return;

            Color32 col = new Color32(30, 144, 255, 255);

            // Redefinition so you can customise it on the fly.
            Vector2 lineHorizontalStart = new Vector2(Screen.width / 2 - crosshairScale, Screen.height / 2);
            Vector2 lineHorizontalEnd = new Vector2(Screen.width / 2 + crosshairScale, Screen.height / 2);

            Vector2 lineVerticalStart = new Vector2(Screen.width / 2, Screen.height / 2 - crosshairScale);
            Vector2 lineVerticalEnd = new Vector2(Screen.width / 2, Screen.height / 2 + crosshairScale);

            ESPUtils.DrawLine(lineHorizontalStart, lineHorizontalEnd, col, lineThickness);
            ESPUtils.DrawLine(lineVerticalStart, lineVerticalEnd, col, lineThickness);
        }
    }
}
