using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Features
{
    internal abstract class SimpleCheat : CheatFeature
    {
        internal void Execute()
        {
            try { OnExecute(); } catch { }
            MelonLogger.Log("Simple Cheat Executed: " + Name);
        }

        protected abstract void OnExecute();

        internal static T AddFeature<T>() where T : SimpleCheat, new()
        {
            var a = new T();
            return a;
        }
    }
}
