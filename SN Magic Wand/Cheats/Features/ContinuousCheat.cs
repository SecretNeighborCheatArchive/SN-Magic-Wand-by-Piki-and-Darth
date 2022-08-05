using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Features
{
    internal abstract class ContinuousCheat : CheatFeature
    {
        private bool _enabled = false;
        internal bool Enabled
        {
            get => _enabled;
            set
            {
                if (value == _enabled) return;
                _enabled = value;
                try { OnEnabledToggle(); } catch { }
                MelonLogger.Log($"Continuous Cheat toggled {(value ? "On" : "Off")}: " + Name);
            }
        }
        internal bool EnabledOnStart = false;

        internal void Toggle()
        {
            Enabled = !_enabled;
        }

        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnEnabledToggle() { }
        protected virtual void OnGUI() { }

        internal abstract EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode { get; }

        internal static T AddFeature<T>(bool enabledOnDefault) where T : ContinuousCheat, new()
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
                    if (Main.CurrentGamemode == f.Gamemode)
                    {
                        f.Enabled = f.EnabledOnStart;
                        try { f.OnStart(); } catch { }
                    }
                    else 
                        f.Enabled = false;

            lastGamemode = Main.CurrentGamemode;

            foreach (var f in fts)
                if (f.Enabled)
                    try { f.OnUpdate(); } catch { }
        }

        // RENAME THIS ASAP
        internal static void UpdateGUI()
        {
            foreach (var feature in fts)
                if (feature.Enabled)
                    try { feature.OnGUI(); } catch { }
        }

        private static EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique lastGamemode;
        private static readonly List<ContinuousCheat> fts = new List<ContinuousCheat>();
    }
}
