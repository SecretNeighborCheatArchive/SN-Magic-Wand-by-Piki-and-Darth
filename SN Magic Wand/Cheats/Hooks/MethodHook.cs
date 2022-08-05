using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Harmony;

namespace SecretNeighbour.Cheats.Hooks
{
    public abstract class MessageHook<T>
    {
        internal void Hook()
        {
            if (hooked) return;
            var harmony = Main.instance.harmonyInstance;
            var methods = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo prefix = Prefix == null ? null : GetType().GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(x => x.Name == Prefix);
            MethodInfo postfix = Postfix == null ? null : GetType().GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(x => x.Name == Postfix);
            foreach (var m in methods)
            {
                if (!m.Name.StartsWith(MethodNameStart)) continue;
                harmony.Patch(m, Prefix == null ? null : new HarmonyMethod(prefix), Postfix == null ? null : new HarmonyMethod(postfix));
            }
            hooked = true;
        }

        private bool hooked = false;
        internal abstract string MethodNameStart { get; }
        internal virtual string Prefix { get; }
        internal virtual string Postfix { get; }
    }
}
