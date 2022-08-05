using GameModes.LobbyMode.LobbyPlayers;
using Harmony;
using MelonLoader;
using SecretNeighbour.Cheats.Gamemodes.Lobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Hooks
{
    public class AntiKick : MessageHook<LobbyPlayer>
    {
        internal override string MethodNameStart => "Method_Public_Void_KickPlayerMessage_PDM";
        internal override string Prefix => "Before";

        public static bool Before([HarmonyArgument(0)] KickPlayerMessage m, ref LobbyPlayer __instance)
        {
            if (!Generic.antiKick || m.author.prop_Boolean_0)
                return true;

            if (__instance == LobbyCheatController.instance.players.localPlayer)
            {
                var plr = LobbyCheatController.instance.players.Players.FirstOrDefault(x => x.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value == m.author.uniqueId._value);

                if (plr != null)
                    MelonLogger.Log($"Blocked kick attempt sent by: {plr.playerInfo.displayName}");
            }
            return false;
        }
    }
}
