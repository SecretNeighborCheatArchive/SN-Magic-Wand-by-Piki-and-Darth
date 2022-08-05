using CodeStage.AntiCheat.ObscuredTypes;
using GameModes.GameplayMode.Interactables.InventoryItems;
using Harmony;
using SecretNeighbour.Cheats.Gamemodes.Gameplay;
using SecretNeighbour.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Hooks
{
    public class RifleMessage : MessageHook<RifleInventoryItem>
    {
        internal override string MethodNameStart => "Method_Protected_Void_ShootMessage_PDM";
        internal override string Prefix => "Before";
        internal override string Postfix => "After";

        public static bool Before(ref RifleInventoryItem __instance, [HarmonyArgument(0)] ShootMessage m)
        {
            if (Generic.infRifleAmmo)
            {
                __instance.cameraShakeAmount = 0f;
                __instance.gravity = 0f;

                if (CheatConfig.current.silentAim)
                {
                    var players = GameplayCheatController.instance.players;
                    var target = players.AllPlayers[new System.Random().Next(players.AllPlayers.Count)];

                    if (target != players.localPlayer)
                    {
                        m.position = target.prop_Actor_0.transform.position;
                        m.direction = target.transform.eulerAngles;
                    }
                }
            }

            return true;
        }

        public static void After(ref RifleInventoryItem __instance, [HarmonyArgument(0)] ShootMessage m)
        {
            if (Generic.infRifleAmmo)
            {
                __instance.field_Protected_ObscuredBool_0 = true;

                if (CheatConfig.current.drawTracers)
                {
                    var hit = CheatUtils.Raycast(m.position, m.direction);

                    if (hit.HasValue)
                    {
                        Vector3 point = hit.Value.point;
                        CheatUtils.DrawTracer(m.position, point, Color.red);
                    }
                }
            }
        }
    }
}
