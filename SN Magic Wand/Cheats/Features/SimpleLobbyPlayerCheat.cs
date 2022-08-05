using BackEnd;
using GameModes.LobbyMode.LobbyPlayers;
using HoloNetwork.Messaging.Implementations;
using MelonLoader;
using SecretNeighbour.Cheats.Gamemodes.Lobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Features
{
    internal abstract class SimpleLobbyPlayerCheat : CheatFeature
    {
        private LobbyPlayer currentPlayer;

        protected abstract void OnExecute(LobbyPlayer player);

        internal void ExecuteOnAll()
        {
            foreach (LobbyPlayer player in LobbyPlayers.Players)
                Execute(player);
        }

        internal void ExecuteOnLocalPlayer()
        {
            Execute(LobbyPlayers.localPlayer);
        }

        internal void Execute(LobbyPlayer player)
        {
            if (player == null)
                return;

            currentPlayer = player;

            try { OnExecute(player); } catch { }

            PlayerInfo pi = player.playerInfo;
            MelonLogger.Log($"Simple LobbyPlayer Cheat Executed: {Name}\nPlayer: {pi.displayName} - {pi.playerID}");
        }

        protected void SendPlayerMessage(HoloNetObjectMessage message, EnumPublicSealedvaOtAlSe5vSeUnique target = EnumPublicSealedvaOtAlSe5vSeUnique.All)
        {
            currentPlayer.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0(message, target);
        }

        protected void SendPlayerMessage(HoloNetObjectMessage message, LobbyPlayer target)
        {
            // Cheeky author bypass.
            message.author = target.prop_HoloNetPlayer_0;

            currentPlayer.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_HoloNetPlayer_0(message, target.prop_HoloNetObject_0.prop_HoloNetPlayer_0);
        }

        internal static T AddFeature<T>() where T : SimpleLobbyPlayerCheat, new()
        {
            var a = new T();
            return a;
        }
    }
}
