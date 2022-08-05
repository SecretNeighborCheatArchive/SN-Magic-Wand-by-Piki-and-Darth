using HoloNetwork.Messaging.Implementations;
using SecretNeighbour.Cheats.Gamemodes.Gameplay;
using SecretNeighbour.Cheats.Gamemodes.Lobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Features
{
    internal abstract class CheatFeature
    {
        internal CheatFeature()
        {
            if (initialized) OnInit();
            features.Add(this);
        }

        internal abstract string Name { get; }

        internal static void Initialize()
        {
            if (initialized) return;
            initialized = true;
            foreach (var f in features.ToArray()) f.OnInit();
        }

        protected virtual void OnInit() { }

        protected static PlayersController Players => GameplayCheatController.instance.players;
        protected static void SendGlobalMessage(HoloNetGlobalMessage message, EnumPublicSealedvaOtAlSe5vSeUnique destinationGroup = EnumPublicSealedvaOtAlSe5vSeUnique.All)
        {
            ObjectPublicDoBoObBoUnique.Method_Public_Static_Void_HoloNetGlobalMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0(message, destinationGroup);
        }
        private static List<CheatFeature> features = new List<CheatFeature>();
        internal static IReadOnlyList<CheatFeature> Features => features;
        private static bool initialized = false;
        protected static LobbyPlayerController LobbyPlayers => LobbyCheatController.instance.players;
    }
}
