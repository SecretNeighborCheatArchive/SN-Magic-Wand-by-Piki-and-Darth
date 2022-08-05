using GameModes.GameplayMode.Interactables.InventoryItems.Base;
using GameModes.GameplayMode.Levels.Basement.Objectives;
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
    internal class BasementDoor : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Basement door ESP";

        private BasementDoorInteractable basementDoor;

        private Vector3 w2s;

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

            if (basementDoor != null)
            {
                w2s = mainCam.WorldToScreenPoint(basementDoor.transform.position);
                w2s.y = Screen.height - (w2s.y + 1f);

                if (ESPUtils.IsOnScreen(w2s))
                    ESPUtils.BoxString(new GUIContent("BASEMENT"), w2s, Color.magenta);
                    //ESPUtils.DrawString1(w2s, "BASEMENT", Color.magenta, true, 10, FontStyle.Bold, 2);
            }
            else // OnStart doesn't work for this, it runs too soon.
            {
                var bDoors = UnityEngine.Object.FindObjectsOfType<BasementDoorInteractable>();
                basementDoor = bDoors.FirstOrDefault(d => d.isMainBasementDoor);
            }
        }
    }
}
