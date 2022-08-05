using GameModes.GameplayMode.Players;
using Harmony;
using MelonLoader;
using SecretNeighbour.Cheats.Gamemodes.Gameplay;
using SecretNeighbour.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Hooks
{
    public class AntiKill : MessageHook<Object1PublicBeAcUnique>
    {
        /*[HarmonyPatch(typeof(Object1PublicBeAcUnique))]
        [HarmonyPatch("Method_Protected_Void_HandleDeathMessage_PDM_0")]
        class AntiKillPatch
        {
            public static bool enabled = true;

            static bool Prefix([HarmonyArgument(0)] HandleDeathMessage m, ref Object1PublicBeAcUnique __instance)
            {
                if (m.author.prop_Boolean_0)
                    return true;

                if (__instance.prop_HoloNetObject_0.IsLocal)
                {
                    var plr = GameplayCheatController.instance.players.AllPlayers.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

                    MelonLogger.LogWarning($"Blocked death message with deathReason: '{m.deathReason}' Sent by: {plr.prop_PlayerInfo_0.displayName}");

                    return false;
                }

                return !enabled;
            }
        }*/

        internal override string MethodNameStart => "Method_Protected_Void_HandleDeathMessage_PDM";
        internal override string Prefix => "Before";

        internal static bool enabled = true;

        public static bool Before([HarmonyArgument(0)] HandleDeathMessage m, ref Object1PublicBeAcUnique __instance)
        {
            try
            {
                if (!CheatConfig.current.antiKill)
                    return true;

                if (m.author == null || __instance.prop_HoloNetObject_0 == null)
                    return true;

                if (m.author.prop_Boolean_0)
                    return true;

                if (__instance.prop_HoloNetObject_0.IsLocal)
                {
                    var plr = GameplayCheatController.instance.players.AllPlayers.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

                    MelonLogger.LogWarning($"Blocked death message with deathReason: '{m.deathReason}' Sent by: {plr.prop_PlayerInfo_0.displayName}");

                    return false;
                }

                return !enabled;
            }
            catch { }

            return true;
        }
    }
}
