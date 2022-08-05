using BackEnd;
using GameModes.GameplayMode.Players;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Features
{
    internal abstract class ContinuousPlayerCheat : CheatFeature
    {
        internal bool EnabledOnStart = false;

        internal void ToggleAll(bool enabled)
        {
            foreach (var p in players.ToArray()) if (p.Key != null) Toggle(p.Key, enabled);
        }

        internal void ToggleAll()
        {
            foreach (var p in players.ToArray()) if (p.Key != null) Toggle(p.Key);
        }

        internal void Toggle(Player player)
        {
            if (!players.ContainsKey(player)) return;
            Toggle(player, !players[player]);
        }

        internal void Toggle(Player player, bool enabled)
        {
            if (!players.ContainsKey(player)) return;
            if (players[player] == enabled) return;
            players[player] = enabled;
            try { OnEnabledToggle(player, enabled); } catch { }
            PlayerInfo pi = player.prop_PlayerInfo_0;
            MelonLogger.Log($"Continuous Player Cheat toggled {(enabled ? "On" : "Off")}: {Name}\nPlayer: {pi.displayName} - {pi.playerID}");
        }

        protected virtual void OnStart(Player player) { }
        protected virtual void OnUpdate(Player player) { }
        protected virtual void OnEnabledToggle(Player player, bool enabled) { }
        protected virtual void OnGUI(Player player) { }

        internal static T AddFeature<T>(bool enabledOnDefault) where T : ContinuousPlayerCheat, new()
        {
            var a = new T();
            fts.Add(a);

            a.EnabledOnStart = enabledOnDefault;

            return a;
        }

        //Update functions
        internal static void Update()
        {
            if (lastGamemode != Main.CurrentGamemode)
                foreach (var f in fts)
                    if (Main.CurrentGamemode == EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY)
                    {
                        foreach (Player p in Players.AllPlayers) f.players.Add(p, f.EnabledOnStart);
                        foreach (var p in f.players.ToArray()) try { f.OnStart(p.Key); } catch { }
                    }
                    else
                        f.players.Clear();

            lastGamemode = Main.CurrentGamemode;

            foreach (var f in fts)
            {
                foreach (var p in f.players.ToArray())
                {
                    if (p.Key == null) continue;
                    if (p.Value) try { f.OnUpdate(p.Key); } catch { }
                }
            }
        }

        internal static void UpdateGUI()
        {
            foreach (var f in fts)
            {
                foreach (var p in f.players.ToArray())
                {
                    if (p.Key == null) continue;
                    if (p.Value) try { f.OnGUI(p.Key); } catch { }
                }
            }
        }

        private static EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique lastGamemode;
        private Dictionary<Player, bool> players = new Dictionary<Player, bool>();
        internal IReadOnlyDictionary<Player, bool> EnabledPlayers => players;
        private static readonly List<ContinuousPlayerCheat> fts = new List<ContinuousPlayerCheat>();
    }
}
