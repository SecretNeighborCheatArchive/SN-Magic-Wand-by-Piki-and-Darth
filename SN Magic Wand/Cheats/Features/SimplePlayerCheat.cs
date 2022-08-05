using BackEnd;
using GameModes.GameplayMode.Players;
using HoloNetwork.Messaging.Implementations;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Features
{
    internal abstract class SimplePlayerCheat : CheatFeature
    {
        internal void ExecuteOnAll()
        {
            foreach (Player p in Players.AllPlayers)
            {
                Execute(p);
            }
        }

        internal void ExecuteOnLocalPlayer()
        {
            Execute(Players.localPlayer);
        }

        internal void Execute(Player player)
        {
            if (player == null) return;
            currentPlayer = player;
            PlayerInfo pi = player.prop_PlayerInfo_0;
            try { OnExecute(player); }
            catch (Exception ex)
            {
                MelonLogger.LogError($"Failed to execute a Simple Player Cheat: {Name}\nPlayer: {pi.displayName} - {pi.playerID}\n" + ex.ToString());
                return;
            }
            MelonLogger.Log($"Simple Player Cheat Executed: {Name}\nPlayer: {pi.displayName} - {pi.playerID}");
        }

        protected abstract void OnExecute(Player player);

        protected void SendPlayerMessage(HoloNetObjectMessage message, EnumPublicSealedvaOtAlSe5vSeUnique target = EnumPublicSealedvaOtAlSe5vSeUnique.All)
        {
            currentPlayer.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0(message, target);
        }

        protected void SendPlayerMessage(HoloNetObjectMessage message, Player target)
        {
            currentPlayer.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_HoloNetPlayer_0(message, target.prop_HoloNetObject_0.prop_HoloNetPlayer_0);
        }

        private Player currentPlayer;
        internal static T AddFeature<T>() where T : SimplePlayerCheat, new()
        {
            var a = new T();
            return a;
        }
    }
}
