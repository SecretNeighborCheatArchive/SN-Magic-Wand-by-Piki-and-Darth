using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Gamemode = EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique;
using SecretNeighbour.Cheats.Gamemodes;
using SecretNeighbour.Cheats.Gamemodes.Gameplay;
using SecretNeighbour.Cheats.Gamemodes.Lobby;
using AppControllers;
using SecretNeighbour.Cheats.Features;
using SecretNeighbour.Cheats;
using System.Diagnostics;
using System.Collections;
using SecretNeighbour.Menu;
using System.Runtime.InteropServices;
using SecretNeighbour.Configs;
using UnityEngine;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using GameModes.GameplayMode.Players;
using System.Security.Principal;
using UnhollowerBaseLib;
using SecretNeighbour.Cheats.Hooks;
using Harmony;
using Assets.Scripts.AppControllers.Config;
using Configuration;
using Configuration.Configs.Debug;
using Configuration.Configs;
using Object = UnityEngine.Object;
using Configuration.Configs.Gameplay;
using Photon.Pun;
using SecretNeighbour.Extra;

namespace SecretNeighbour
{
    internal static class BuildInfo
    {
        internal const string Name = "Secret Neighbour Cheats"; // Name of the Mod.  (MUST BE SET)
        internal const string Description = "Mod for Testing"; // Description for the Mod.  (Set as null if none)
        internal const string Author = "Magic Wand Owner & Piki"; // Author of the Mod.  (Set as null if none)
        internal const string Company = ""; // Company that made the Mod.  (Set as null if none)
        internal const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        internal const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class StopDecompilingOurFuckingAssembly { }

    public class Main : MelonMod
    {
        internal static Player yobitch;

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool FreeLibrary(IntPtr hModule);

        public unsafe Main()
        {
                                    //CheatConfig.current = CheatConfig.Load();
                                    gameModesCheats = new IGameMode[]
                                    {
                                        new GameplayCheatController(),
                                        new LobbyCheatController()
                                    };

                                    yobitch = new Player();

                                    instance = this;
                                    return;
            var text1 = SystemInfo.deviceUniqueIdentifier;
            string retardedText1 = string.Empty;
            int l1, i1;
            char ch1;
            l1 = text1.Length;
            var arr = text1.ToCharArray();
            for (i1 = 0; i1 < l1; i1++)
            {
                ch1 = arr[i1];
                if (Char.IsLower(ch1))
                    retardedText1 += Char.ToUpper(ch1);
                else
                    retardedText1 += Char.ToLower(ch1);
            }
            string rawtext1 = text1 + "ab=ba" + retardedText1;
            string convertor1 = string.Empty;
            int times1 = new System.Random().Next(4, 7);
            foreach (char str in rawtext1.ToCharArray())
            {
                convertor1 += (str * times1).ToString();
            }
            char[] thingChars1 = convertor1.ToCharArray();
            Array.Reverse(thingChars1);
            string id = new string(thingChars1) + times1.ToString();
            MelonLogger.Log(id);
            System.Windows.Forms.Clipboard.SetText(id);
            Application.Quit();
            System.Windows.Forms.MessageBox.Show("Looks like you haven't purchased a license yet.\nIn case you've just bought a license, we have copied your hardware id to the clipboard which you have to send to your current seller.", "SN Magic Wand", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            for (;;)
            {
                Application.Quit();
                Environment.Exit(0);
                Process.GetCurrentProcess().Kill();
            }
        }

        [HarmonyPatch(typeof(Il2CppSystem.IO.Directory))]
        [HarmonyPatch("InternalGetFileDirectoryNames")]
        public class Hologrpyh
        {
            public static void Postfix(ref Il2CppStringArray __result)
            {
                __result = new Il2CppStringArray(new string[2] { "GameAssembly.dll", "UnityPlayer.dll" });
            }
        }

        internal static IntPtr GetModuleBaseAddress(string moduleName)
        {
            Process process = Process.GetCurrentProcess();

            var module = process.Modules.Cast<ProcessModule>().SingleOrDefault(m => string.Equals(m.ModuleName, moduleName, StringComparison.OrdinalIgnoreCase));

            return module?.BaseAddress ?? IntPtr.Zero;
        }

        [HarmonyPatch(typeof(Application))]
        [HarmonyPatch("dataPath", MethodType.Getter)]
        public class TestPatch
        {
            public static void Postfix(ref string __result)
            {
                l++;
                if (instance == null || l == 2 || CurrentGamemode != Gamemode.PRELOAD) 
                    return;

                string path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), @"MelonLoader\SN");
                __result = Path.Combine(path, "Secret Neighbour_Data");
                Directory.CreateDirectory(__result);
                string[] a = new string[] { Path.Combine(path, "GameAssembly.dll"), Path.Combine(path, "UnityPlayer.dll") };
                foreach (string b in a)
                {
                    if (!File.Exists(b)) File.Create(b).Close();
                }

                /*IntPtr moduleBase = GetModuleBaseAddress("version.dll");

                if (moduleBase != IntPtr.Zero)
                {
                    FreeLibrary(moduleBase);

                    if (File.Exists(__result + @"\version.dll"))
                        File.Move(__result + @"\version.dll", __result + @"\version");

                    MelonLogger.Log("Freed version.dll and moved it.");
                }
                else
                    MelonLogger.Log("Coudln't free version.dll.");

                MelonCoroutines.Start(RenameVersionDLL(__result));*/
            }

            static uint l = 0;
        }

        /*[HarmonyPatch(typeof(Application))]
        [HarmonyPatch("persistentDataPath", MethodType.Getter)]
        public class TestPatchTwo
        {
            static void Postfix(ref string __result)
            {
                MelonLogger.Log($"Application.get_persistentDataPath() OLD: {__result}");

                __result = @"C:\Program Files (x86)\Steam\steamapps\common\Secret Neighbour_Fake\Secret Neighbor";
            }
        }*/

        [HarmonyPatch(typeof(PunishmentConfig))]
        [HarmonyPatch(MethodType.Constructor)]
        public class NoShadowBan
        {
            public static void Update()
            {
                if (GameContext.instance != null)
                    GameContext.instance.isGameSafeToLeave = true;
            }

            //public static void Postfix(ref PunishmentConfig __instance)
            //{
            //    MelonLogger.Log("Shadow ban bypassed.");
            //}
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        public override void OnApplicationStart()
        {            
            Console.Title = Guid.NewGuid().ToString();

            instance = this;

            new RifleMessage().Hook();
            //new AntiTP().Hook();
            //new AntiKill().Hook();
            //new SteamIDSpoofer().Hook();
            new Cheats.Hooks.AntiKick().Hook();

            IL2CPPAssetBundle bundle = new IL2CPPAssetBundle();

            //if (bundle.LoadBundle("SecretNeighbour.outline"))
            //{
            //    MelonLogger.Log("Outline bundle loaded.");
            //    CheatUtils.chamsOutline = bundle.Load<Shader>("outline");
            //    MelonLogger.Log("Outline shader loaded.");
            //}
            //else
                //MelonLogger.Log("Outline bundle failed to load.");

            CheatFeature.Initialize();
            ESPUtils.Init();

            //var discordSDKLoaded = LoadLibrary(Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), @"Secret Neighbour_Data\Plugins\x86_64\discord_game_sdk.dll"));
            //if (discordSDKLoaded != IntPtr.Zero)
            //    DiscordRP.Init();
            //else
            //    MelonLogger.LogError("Failed to load the Discord SDK Plugin");

            //SceneDebugger.Start();
        }

        public override void OnApplicationQuit()
        {
            DiscordRP.Dispose();
        }

        public override void OnGUI()
        {
            ContinuousCheat.UpdateGUI();
            ContinuousPlayerCheat.UpdateGUI();

            //SceneDebugger.OnGUI();
            
            if (activeCheatMode != null)
                activeCheatMode.OnGUI();
        }

        public override void OnFixedUpdate()
        {
            if (activeCheatMode != null) 
                activeCheatMode.FixedUpdate();
        }

        public override void OnUpdate()
        {

            CurrentGamemode = GetCurrentGamemode;

            if (CurrentGamemode != gamemodeBeforeUpdate)
            {
                DiscordRP.OnLevelChanged(CurrentGamemode);

                var gmCheatToStart = GetCheatModeForGamemode(CurrentGamemode);

                if (activeCheatMode != null) 
                    activeCheatMode.End();

                activeCheatMode = gmCheatToStart;

                if (gmCheatToStart != null) 
                    gmCheatToStart.Start();
            }

            gamemodeBeforeUpdate = CurrentGamemode;

            if (activeCheatMode != null) 
                activeCheatMode.Update();

            ContinuousCheat.Update();
            ContinuousPlayerCheat.Update();

            Objects.Items.Update();
            DiscordRP.Update();

            NoShadowBan.Update();
            CheatUtils.Update();
        }

        private IGameMode GetCheatModeForGamemode(Gamemode gm)
        {
            return gameModesCheats.FirstOrDefault(x => x.Gamemode == gm);
        }

        private readonly IGameMode[] gameModesCheats;

        internal static Main instance { get; private set; }
        private Gamemode GetCurrentGamemode => AppController.prop_AppController_0.modes.prop_EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique_0;
        private IGameMode activeCheatMode;
        internal static Gamemode CurrentGamemode;
        internal static Gamemode gamemodeBeforeUpdate;
    }
}
