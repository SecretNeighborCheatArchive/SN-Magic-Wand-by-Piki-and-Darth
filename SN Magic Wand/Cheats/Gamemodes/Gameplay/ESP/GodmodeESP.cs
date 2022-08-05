using GameModes.GameplayMode.Players;
using SecretNeighbour.Cheats.Features;
using SecretNeighbour.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay.ESP
{
    internal class GodmodeESP : ContinuousPlayerCheat
    {
        internal override string Name => "Godmode ESP";

        private Camera mainCam = Camera.main;

        protected override void OnStart(Player player)
        {
            mainCam = Camera.main;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnGUI(Player player)
        {
            // Only paint once per frame, save FPS.
            if (Event.current.type != EventType.Repaint)
                return;

            if (CheatConfig.current.godmodeESP)
                if (player != Players.localPlayer)
                    if (player.field_Private_InterfacePublicAbstractBoObSiBoObObBoObUnique_0.Method_Public_Abstract_Virtual_New_Boolean_EnumPublicSealedvaSTCAGLCADITODIKNSLUnique_0(EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.INVINCIBLE))
                    {
                        Vector2 w2s = mainCam.WorldToScreenPoint(player.prop_Actor_0.transform.position);
                        w2s.y = Screen.height - (w2s.y + 1f) - 7f;

                        ESPUtils.DrawString1(w2s, "GODMODE", Color.magenta, true, 10, FontStyle.Bold, 2);
                    }
        }
    }
}
