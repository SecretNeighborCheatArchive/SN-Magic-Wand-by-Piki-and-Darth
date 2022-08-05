using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class KeyNameESP : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Key ESP";

        private Camera mainCam = Camera.main;

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

            if (Objects.Items.keys.Length > 0)
                foreach (var key in Objects.Items.keys)
                {
                    if (key == null)
                        continue;

                    var keyPosition = key.transform.position;
                    var w2s = mainCam.WorldToScreenPoint(keyPosition);

                    if (ESPUtils.IsOnScreen(w2s))
                    {
                        w2s.y = Screen.height - (w2s.y + 1f);

                        string keyName = key.keyType.ToString();
                        Color color = GetKeyColor(keyName);

                        ESPUtils.BoxString(new GUIContent(keyName), w2s, color);
                    }
                }
        }

        private Color GetKeyColor(string keyName)
        {
            Color color = Color.cyan;

            switch (keyName.ToUpper())
            {
                case "RED":
                    color = Color.red;
                    break;
                case "YELLOW":
                    color = Color.yellow;
                    break;
                case "BLUE":
                    color = Color.blue;
                    break;
            }

            return color;
        }
    }
}
