using MelonLoader;
using Harmony;
using GameModes.GameplayMode.Interactables.InventoryItems;
using Ui.Screens.CustomGame;
using GameModes.LobbyMode;
using Steamworks;
using GameModes.LobbyMode.LobbyPlayers;
using SecretNeighbour.Cheats.Gamemodes.Lobby;
using UnityEngine;
using SecretNeighbour.Cheats.Gamemodes.Gameplay;
using GameModes.GameplayMode.Actors;
using SecretNeighbour.Configs;
using System.Linq;
using GameModes.GameplayMode.Players;
using GameModes.GameplayMode;
using HoloNetwork.Players;
using System.IO;
using AppControllers;
using System.Diagnostics;

namespace SecretNeighbour.Cheats.Hooks
{
    public class Generic
    {
        // TODO: move these to a better place.
        internal static string customSteamName = "High Powered Magic Wand Owner";
        internal static bool infRifleAmmo = true;
        internal static bool antiKick = true;
        internal static bool antiKill = true;

        //[HarmonyPatch(typeof(Object1PublicAbstractPlUnique))]
        //[HarmonyPatch("Method_Protected_Void_DecideDeathMessage_PDM_0")]
        //public class AntiKill
        //{
        //    public static bool Prefix([HarmonyArgument(0)] DecideDeathMessage m, ref Object1PublicAbstractPlUnique __instance)
        //    {
        //        if (antiKill)
        //        {
        //            if (m.author != null)
        //                if (m.author.prop_Boolean_0)
        //                    return true;

        //            if (__instance.prop_HoloNetObject_0.IsLocal)
        //            {
        //                var plr = GameplayCheatController.instance.players.AllPlayers.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

        //                if (plr == null)
        //                    return false;

        //                MelonLogger.LogWarning($"Blocked death message with deathReason: '{m.deathReason}' Sent by: {plr.prop_PlayerInfo_0.displayName}");

        //                return false;
        //            }

        //            return true;
        //        }

        //        return true;
        //    }
        //}

        //[HarmonyPatch(typeof(RifleInventoryItem), nameof(RifleInventoryItem.Method_Protected_Void_ShootMessage_PDM_0))]
        //public class InfiniteRifleAmmo
        //{
        //    public static void Prefix(RifleInventoryItem __instance, [HarmonyArgument(0)] ShootMessage m)
        //    {
        //        if (infRifleAmmo)
        //        {
        //            __instance.cameraShakeAmount = 0f;
        //            __instance.gravity = 0f;

        //            // Silent aim.
        //            if (CheatConfig.current.silentAim)
        //            {
        //                var players = GameplayCheatController.instance.players;
        //                var target = players.AllPlayers[new System.Random().Next(players.AllPlayers.Count)];

        //                if (target != players.localPlayer)
        //                {
        //                    m.position = target.prop_Actor_0.transform.position;
        //                    m.direction = target.transform.eulerAngles;
        //                }
        //            }
        //        }
        //    }

        //    public static void Postfix(ref RifleInventoryItem __instance, [HarmonyArgument(0)] ShootMessage m)
        //    {
        //        if (infRifleAmmo)
        //        {
        //            __instance.field_Protected_ObscuredBool_0 = true;

        //            if (CheatConfig.current.drawTracers)
        //            {
        //                var hit = CheatUtils.Raycast(m.position, m.direction);

        //                if (hit.HasValue)
        //                {
        //                    Vector3 point = hit.Value.point;
        //                    CheatUtils.DrawTracer(m.position, point, Color.red);
        //                }
        //            }
        //        }
        //    }
        //}
            
        /*[HarmonyPatch(typeof(LobbyPlayer))]
        [HarmonyPatch("Method_Public_Void_KickPlayerMessage_PDM_0")]
        class KickProtection
        {
            private static bool Prefix([HarmonyArgument(0)] KickPlayerMessage m, ref LobbyPlayer __instance)
            {
                if (!antiKick || m.author.prop_Boolean_0)
                    return true;

                if (__instance == LobbyCheatController.instance.players.localPlayer)
                {
                    var plr = LobbyCheatController.instance.players.Players.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

                    if (plr != null)
                        MelonLogger.Log($"Blocked kick attempt sent by: {plr.playerInfo.displayName}");

                    return false;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(LobbyPlayer))]
        [HarmonyPatch("Method_Public_Void_KickPlayerMessage_PDM_1")]
        class KickProtection1
        {
            private static bool Prefix([HarmonyArgument(0)] KickPlayerMessage m, ref LobbyPlayer __instance)
            {
                if (!antiKick || m.author.prop_Boolean_0)
                    return true;

                if (__instance == LobbyCheatController.instance.players.localPlayer)
                {
                    var plr = LobbyCheatController.instance.players.Players.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

                    if (plr != null)
                        MelonLogger.Log($"Blocked kick attempt sent by: {plr.playerInfo.displayName}");

                    return false;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(LobbyPlayer))]
        [HarmonyPatch("Method_Public_Void_KickPlayerMessage_PDM_2")]
        class KickProtection2
        {
            private static bool Prefix([HarmonyArgument(0)] KickPlayerMessage m, ref LobbyPlayer __instance)
            {
                if (!antiKick || m.author.prop_Boolean_0)
                    return true;

                if (__instance == LobbyCheatController.instance.players.localPlayer)
                {
                    var plr = LobbyCheatController.instance.players.Players.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

                    if (plr != null)
                        MelonLogger.Log($"Blocked kick attempt sent by: {plr.playerInfo.displayName}");

                    return false;
                }

                return false;
            }
        }*/

        /*[HarmonyPatch(typeof(SteamFriends))]
        [HarmonyPatch("GetPersonaName")]
        class SteamNameSpoofer
        {
            static void Postfix(ref string __result)
            {
                MelonLogger.Log($"Old steam name: {__result}");

                __result = customSteamName;

                MelonLogger.Log($"New steam name: {__result}");
            }
        }*/

        /*[HarmonyPatch(typeof(Application))]
        [HarmonyPatch("Quit", new System.Type[] { typeof(int) })]
        class MelonBypass
        {
            static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(Application))]
        [HarmonyPatch("Quit", new System.Type[] { })]
        class MelonBypassTwo
        {
            static bool Prefix()
            {
                //I like ya anti-cheat g's
                // they do some other checks in the same place, because every now and then you'll get `Network Connection Failure` but you can still play sometimes. it's hit and miss.
                // i'm gonna IDA it rn
                return false;
            }
        }*/

        [HarmonyPatch(typeof(CustomGameScreen))]
        [HarmonyPatch("Method_Private_Void_List_1_Object1PublicStInStBoUnique_0")]
        public class NoPasswords
        {
            public static bool Prefix(ref CustomGameScreen __instance)
            {
                LobbyController.prop_LobbyController_0.Method_Public_Void_ObjectPublicStInStBoObStBoInBoStUnique_0(__instance.field_Private_ObjectPublicStInStBoObStBoInBoStUnique_0); 

                return false;
            }
        }

        //[HarmonyPatch(typeof(Object1PublicAbstractAcObLiOb1LeObBoAcInUnique))]
        //[HarmonyPatch("Method_Public_get_Boolean_1")]
        //public class NoSkillCooldowns
        //{
        //    public static bool Prefix(ref bool __result)
        //    {
        //        if (CheatConfig.current.noSkillCooldowns)
        //        {
        //            __result = false;
        //            return false;
        //        }

        //        return true;
        //    }
        //}

        /*[HarmonyPatch(typeof(ObjectPublicDi2ObInHa1StUnique))]
        [HarmonyPatch("Method_Public_Boolean_String_PDM_6")]
        class UnlockAll
        {
            static bool Prefix(ref bool __result, ref ObjectPublicDi2ObInHa1StUnique __instance)
            {
                __result = true;

                return false;
            }
        }*/
    }
}
