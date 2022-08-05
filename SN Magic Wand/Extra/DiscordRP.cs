using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamemode = EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique;
using Discord;
using Photon.Pun;
using SecretNeighbour.Utils;
using Harmony;
using SecretNeighbour.Cheats.Hooks;
using GameModes.LobbyMode.LobbyPlayers;
using GameModes.LobbyMode;
using GameModes.GameplayMode.Levels.Modules;
using GameModes.LobbyMode.LobbyPlayers.Messages;

namespace SecretNeighbour.Extra
{
    internal static class DiscordRP
    {
        internal static ActivityManager activity;
        internal static Activity currentActivity;
        internal static Discord.Discord discord;
        internal const long appId = 840323444981366824;
        public static bool initialized = false;

        internal static void Init()
        {
            discord = new Discord.Discord(appId, (ulong)CreateFlags.NoRequireDiscord);
            activity = discord.GetActivityManager();

            //new OnGameStart().Hook();
            new OnLevelTimerSet().Hook();

            currentActivity.Assets.LargeImage = "raven_512";
            currentActivity.State = "In Splashscreen";
            activity.UpdateActivity(currentActivity, (result) => { });


            initialized = true;
        }

        internal static void OnLevelChanged(Gamemode gamemode)
        {
            if (!initialized)
                return;
            currentActivity.Secrets = default;
            if (gamemode != Gamemode.GAMEPLAY)
                currentActivity.Timestamps = default;
            switch (gamemode)
            {
                case Gamemode.PRELOAD:
                    currentActivity.State = "In Main Menu";
                    break;
                case Gamemode.MENU:
                    currentActivity.State = "In Menu";
                    break;
                case Gamemode.LOBBY:
                    currentActivity.Timestamps.Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    currentActivity.State = "In Lobby";
                    currentActivity.Secrets.Join = "myass";
                    break;
                case Gamemode.SHOP:
                    currentActivity.State = "In Shop";
                    break;
                case Gamemode.GAME_RESULTS:
                    currentActivity.State = "In Game Results";
                    break;
                case Gamemode.MAP_EDITOR:
                    currentActivity.State = "In Map Editor";
                    break;
                case Gamemode.GAMEPLAY:
                    currentActivity.State = "In Game";
                    break;
            }

            activity.UpdateActivity(currentActivity, (result) => { Console.WriteLine(result.ToString()); });
        }

        internal static void Dispose()
        {
            if (!initialized)
                return;
            discord.Dispose();
        }

        internal static void Update()
        {
            if (!initialized)
                return;
            discord.RunCallbacks();
        }

        [HarmonyPatch(typeof(ObjectPublicIConnectionCallbacksIMatchmakingCallbacksIInRoomCallbacksILobbyCallbacksIOnEventCallbackObBoObStStBoObBoObBoUnique))]
        [HarmonyPatch("OnJoinedRoom")]
        public static class OnJoinedRoom
        {
            public static void Postfix()
            {
                if (!initialized)
                    return;
                var room = PhotonNetwork.CurrentRoom;
                currentActivity.Instance = true;
                currentActivity.Party.Id = room.Name;
                currentActivity.Party.Size.CurrentSize = room.PlayerCount;
                currentActivity.Party.Size.MaxSize = room.MaxPlayers;
                currentActivity.Details = room.GetRealName();

                activity.UpdateActivity(currentActivity, (result) => { Console.WriteLine(result.ToString()); });
            }
        }

        [HarmonyPatch(typeof(ObjectPublicIConnectionCallbacksIMatchmakingCallbacksIInRoomCallbacksILobbyCallbacksIOnEventCallbackObBoObStStBoObBoObBoUnique))]
        [HarmonyPatch("OnLeftRoom")]
        public static class OnLeftRoom
        {
            public static void Postfix()
            {
                if (!initialized)
                    return;
                currentActivity.Instance = false;
                currentActivity.Party = default;
                currentActivity.Details = default;

                activity.UpdateActivity(currentActivity, (result) => { Console.WriteLine(result.ToString()); });
            }
        }

        [HarmonyPatch(typeof(ObjectPublicIConnectionCallbacksIMatchmakingCallbacksIInRoomCallbacksILobbyCallbacksIOnEventCallbackObBoObStStBoObBoObBoUnique))]
        [HarmonyPatch("OnPlayerEnteredRoom")]
        public static class OnPlayerEnteredRoom
        {
            public static void Postfix()
            {
                if (!initialized)
                    return;
                currentActivity.Party.Size.CurrentSize = PhotonNetwork.CurrentRoom.PlayerCount;

                activity.UpdateActivity(currentActivity, (result) => { Console.WriteLine(result.ToString()); });
            }
        }

        [HarmonyPatch(typeof(ObjectPublicIConnectionCallbacksIMatchmakingCallbacksIInRoomCallbacksILobbyCallbacksIOnEventCallbackObBoObStStBoObBoObBoUnique))]
        [HarmonyPatch("OnPlayerLeftRoom")]
        public static class OnPlayerLeftRoom
        {
            public static void Postfix()
            {
                if (!initialized)
                    return;
                currentActivity.Party.Size.CurrentSize = PhotonNetwork.CurrentRoom.PlayerCount;

                activity.UpdateActivity(currentActivity, (result) => { Console.WriteLine(result.ToString()); });
            }
        }

        
    }

    //public class OnGameStart : MessageHook<LobbyPlayer>
    //{
    //    internal override string MethodNameStart => "Method_Public_Void_LobbyPlayerChangeStateMessage_PDM";
    //    internal override string Postfix => "After";

    //    public static void After([HarmonyArgument(0)] LobbyPlayerChangeStateMessage state)
    //    {
    //        MelonLoader.MelonLogger.LogWarning(state.newState.ToString());
    //        return;
    //        DiscordRP.currentActivity.State = "In Loading Screen";
    //        DiscordRP.currentActivity.Secrets = default;
    //        DiscordRP.currentActivity.Timestamps.Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    //        DiscordRP.activity.UpdateActivity(DiscordRP.currentActivity, (result) => { Console.WriteLine(result.ToString()); });
    //    }
    //}

    public class OnLevelTimerSet : MessageHook<Object1PublicMaTiObDoObInDoSiSiSiUnique>
    {
        internal override string MethodNameStart => "Method_Private_Void_LevelTimerStartedMessage_PDM_";
        internal override string Postfix => "After";

        public static void After([HarmonyArgument(0)] LevelTimerStartedMessage timer)
        {
            if (!DiscordRP.initialized)
                return;
            DiscordRP.currentActivity.Timestamps = default;
            DiscordRP.currentActivity.Timestamps.End = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long)timer.time;

            DiscordRP.activity.UpdateActivity(DiscordRP.currentActivity, (result) => { Console.WriteLine(result.ToString()); });
        }
    }
}
