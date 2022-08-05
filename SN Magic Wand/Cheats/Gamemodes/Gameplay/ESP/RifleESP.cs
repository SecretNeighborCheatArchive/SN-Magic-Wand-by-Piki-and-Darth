using SecretNeighbour.Cheats.Features;
using SecretNeighbour.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class RifleESP : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Rifle ESP";

        private Camera mainCam = Camera.main;

        private GUIContent rifleName = new GUIContent("RIFLE");

        protected override void OnStart()
        {
            mainCam = Camera.main;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnGUI()
        {
            // Only paint once per frame, save FPS.
            if (Event.current.type != EventType.Repaint)
                return;

            if (Objects.Items.rifles.Length > 0)
                foreach (var rifle in Objects.Items.rifles)
                {
                    if (rifle == null)
                        continue;
                    
                    Vector3 riflePosition = rifle.transform.position;
                    Vector3 w2s = mainCam.WorldToScreenPoint(riflePosition);

                    if (ESPUtils.IsOnScreen(w2s))
                    {
                        w2s.y = Screen.height - (w2s.y + 1f);

                        ESPUtils.BoxString(rifleName, w2s, Color.magenta);
                    }
                }
        }
    }
}
