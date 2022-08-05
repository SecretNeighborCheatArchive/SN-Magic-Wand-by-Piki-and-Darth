using GameModes.GameplayMode.Actors;
using GameModes.GameplayMode.Actors.Shared.Configuration;
using GameModes.GameplayMode.Interactables.InventoryItems;
using GameModes.GameplayMode.Players;
using MelonLoader;
using SecretNeighbour.Cheats.Features;
using SecretNeighbour.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class Aimbot : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Aimbot";


        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private Camera mainCam = Camera.main;

        internal bool drawFov = true;

        internal float crosshairScale = 7f;
        internal float lineThickness = 1.75f;

        private Vector2 targetPos = Vector2.zero;

        protected override void OnStart()
        {
            mainCam = Camera.main;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnGUI()
        {
            if (Event.current.type != EventType.Repaint)
                return;

            if (drawFov)
                ESPUtils.DrawCircle(new Color32(30, 144, 255, 255), new Vector2(Screen.width / 2, Screen.height / 2), CheatConfig.current.fov);

            if (targetPos == Vector2.zero || !CheatConfig.current.aimXhair)
                return;
                
            Color32 col = Color.red;

            Vector2 lineHorizontalStart = new Vector2(targetPos.x - crosshairScale, targetPos.y);
            Vector2 lineHorizontalEnd = new Vector2(targetPos.x + crosshairScale, targetPos.y);

            Vector2 lineVerticalStart = new Vector2(targetPos.x, targetPos.y - crosshairScale);
            Vector2 lineVerticalEnd = new Vector2(targetPos.x, targetPos.y + crosshairScale);

            ESPUtils.DrawLine(lineHorizontalStart, lineHorizontalEnd, col, lineThickness);
            ESPUtils.DrawLine(lineVerticalStart, lineVerticalEnd, col, lineThickness);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                float minDist = CheatConfig.current.aimDist;

                Vector2 target = Vector2.zero;

                foreach (var player in Players.AllPlayers)
                    if (player != Players.localPlayer)
                    {
                        Vector3 lookAt = player.prop_Actor_0.prop_Transform_0.position;

                        var w2s = mainCam.WorldToScreenPoint(lookAt);

                        // If they're outside of our FOV.
                        if (Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(w2s.x, w2s.y)) > CheatConfig.current.fov)
                            continue;

                        if (w2s.z > 0f)
                        {
                            float distance = Math.Abs(Vector2.Distance(new Vector2(w2s.x, Screen.height - w2s.y), new Vector2(Screen.width / 2, Screen.height / 2)));

                            if (distance < minDist)
                            {
                                minDist = distance;
                                target = new Vector2(w2s.x, Screen.height - w2s.y);

                                targetPos = target;
                            }
                        }
                    }

                if (target != Vector2.zero)
                {
                    double distX = target.x - Screen.width / 2f;
                    double distY = target.y - Screen.height / 2f;

                    distX /= 5;
                    distY /= 5;

                    mouse_event(0x0001, (int)distX, (int)distY, 0, 0);
                }
            }
            else
                targetPos = Vector2.zero;
        }
    }
}
