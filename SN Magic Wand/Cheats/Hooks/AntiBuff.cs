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
    //[HarmonyPatch(typeof(Object1PublicLi1ObPlLi1ObLi1ObUnique))]
    //[HarmonyPatch("Method_Private_Void_ApplyBuffByIdMessage_PDM_2")]
    public class AntiBuff : MessageHook<Object1PublicLi1ObPlLi1ObAcLi1Unique>
    {
        internal override string MethodNameStart => "Method_Private_Void_ApplyBuffByIdMessage_PDM";
        internal override string Prefix => "Before";


        public static bool Before([HarmonyArgument(0)] ApplyBuffByIdMessage m, ref Object1PublicHoObObUnique __instance)
        {
            if (!CheatConfig.current.antiTP)
                return true;

            if (m.author == null || __instance.prop_HoloNetObject_0 == null)
                return true;

            if (m.author.prop_Boolean_0) 
                return true;

            if (__instance.prop_HoloNetObject_0.IsLocal && blacklistedBuffs.Contains(m.buff.buffId))
            {
                if (GameplayCheatController.instance.players != null)
                {
                    var plr = GameplayCheatController.instance.players.AllPlayers.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

                    if (plr != null)
                    {
                        MelonLogger.LogWarning($"Blocked buff: '{m.buff.buffId}' because it's blacklisted! Sent by: {plr.prop_PlayerInfo_0.displayName}");
                        return false;
                    }

                    /*if (CheatConfig.current.backfireOtherCheaters)
                    {
                        GameplayCheatController.instance.crash.Execute(plr);

                        MelonLogger.LogWarning($"Crashed: '{plr.prop_PlayerInfo_0.displayName}' because they sent a fraudulent buff message.");
                    }*/
                }
            }

            return true;
        }

        static readonly EnumPublicSealedvaSTCAGLCADITODIKNSLUnique[] blacklistedBuffs = new EnumPublicSealedvaSTCAGLCADITODIKNSLUnique[]
        {
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.BLIND,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.CARRY_HEAVINESS,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.COLD,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.CONTINUES_SLOW,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.DISABLE_ALL_EXCEPT_CAMERA,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.DISABLE_INVENTORY,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.DISABLE_MOVEMENT,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.DISABLE_SKILLS,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.GLUE_SLOW,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.HUNTING_TRAP,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.KNOCK,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.KNOCK_DEAD,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.NEIGHBOR_RESPAWN,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.SIT_ON_CHAIR,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.SLOW,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.SMOKE_GRANADE,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.SPEED_UP_VANISHING,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.TOMATO,
            EnumPublicSealedvaSTCAGLCADITODIKNSLUnique.TRANSFORMATION
        };
    }
}
