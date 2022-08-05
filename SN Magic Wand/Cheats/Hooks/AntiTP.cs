using GameModes.GameplayMode.Actors.Shared;
using Harmony;
using MelonLoader;
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
    public class AntiTP : MessageHook<Object1PublicHoObObUnique>
    {
        internal override string MethodNameStart => "Method_Private_Void_ActorTeleportPositionMessage_PDM";
        internal override string Prefix => "Before";

        public static bool Before([HarmonyArgument(0)] ActorTeleportPositionMessage m, ref Object1PublicHoObObUnique __instance)
        {
            if (!CheatConfig.current.antiTP)
                return true;

            if (m.author == null || __instance.prop_HoloNetObject_0 == null)
                return true;

            if (m.author.prop_Boolean_0) 
                return true;

            if (__instance.prop_HoloNetObject_0.IsLocal)
            {
                var plr = GameplayCheatController.instance.players.AllPlayers.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

                if (plr != null)
                    MelonLogger.LogWarning($"Blocked TP attempt sent by: {plr.prop_PlayerInfo_0.displayName}");

                /*if (CheatConfig.current.backfireOtherCheaters)
                {
                    GameplayCheatController.instance.crash.Execute(plr);

                    MelonLogger.LogWarning($"Crashed: {plr.prop_PlayerInfo_0.displayName} because they tried to TP you!");
                }*/

                return false;
            }

            return true;
        }
    }
}
