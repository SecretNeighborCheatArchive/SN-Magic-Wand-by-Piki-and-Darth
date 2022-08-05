using GameModes.GameplayMode.Actors;
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

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class PlayerNameESP : ContinuousPlayerCheat
    {
        internal override string Name => "Player name ESP";

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

            if (CheatConfig.current.playerNameESP && player != Players.localPlayer)
            {
                Actor actor = player.prop_Actor_0;
                Vector3 w2s = mainCam.WorldToScreenPoint(actor.transform.position);

                w2s.y = Screen.height - (w2s.y + 1f);

                if (ESPUtils.IsOnScreen(w2s))
                {
                    string playerName = player.prop_PlayerInfo_0.displayName;

                    Color color = player.prop_PlayerInfo_0.teamId == EnumPublicSealedvaALBEGADEEPZENECH9vUnique.NEIGHBOR ? Color.red : Color.green;
                    bool godmode = player.field_Private_InterfacePublicAbstractBoObSiBoObObBoObUnique_0.Method_Public_Abstract_Virtual_New_Boolean_EnumPublicSealedvaSTCAGLCADITODIKNSLUnique_0(EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.INVINCIBLE);

                    ESPUtils.BoxString(new GUIContent($"{(godmode ? "<color=magenta>GODMODE</color>\n" : "")}{playerName}"), w2s, color);
                }
            }
        }
    }
}
