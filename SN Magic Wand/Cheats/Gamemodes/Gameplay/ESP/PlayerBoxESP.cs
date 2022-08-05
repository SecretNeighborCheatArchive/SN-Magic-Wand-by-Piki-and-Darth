using GameModes.GameplayMode.Actors;
using GameModes.GameplayMode.Players;
using MelonLoader;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class PlayerBoxESP : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Player Box ESP";

        private readonly float addedHeight = 13f; // Just to make the box actually house the player model.

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

            foreach (var plr in Players.AllPlayers)
                if (plr != Players.localPlayer)
                {
                    Actor actor = plr.prop_Actor_0;

                    Vector3 w2sHead = mainCam.WorldToScreenPoint(actor.prop_Transform_0.position);
                    Vector3 w2sBottom = mainCam.WorldToScreenPoint(actor.transform.position);
                    
                    float height = Mathf.Abs(w2sHead.y - w2sBottom.y);

                    bool neighbour = plr.prop_PlayerInfo_0.teamId == EnumPublicSealedvaALBEGADEEPZENECH9vUnique.NEIGHBOR;

                    Color colour = neighbour ? Color.red : Color.green;

                    if (ESPUtils.IsOnScreen(w2sHead))
                        ESPUtils.CornerBox(new Vector2(w2sHead.x, Screen.height - w2sHead.y - addedHeight), height / 2f, height + addedHeight, 2f, colour, true);
                }
        }
    }
}
