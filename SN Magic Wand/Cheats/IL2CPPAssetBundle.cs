using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SecretNeighbour.Cheats
{
    internal class IL2CPPAssetBundle
    {
        /// <summary>
        /// The Loaded AssetBundle, Null By Default
        /// </summary>
        internal AssetBundle bundle = null;

        internal bool HasLoadedABundle = false;

        /// <summary>
        /// Loads An Asset Bundle For Using Data Such As Sprites
        /// </summary>
        /// <param name="resource">The Path To The Embedded Resource File - Example: VRCAntiCrash.Resources.plaguelogo.asset</param>
        /// <returns>True If Successful</returns>
        internal bool LoadBundle(string resource)
        {
            try
            {
                if (string.IsNullOrEmpty(resource))
                {
                    return false;
                }

                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

                if (stream != null)
                {
                    var memStream = new MemoryStream((int)stream.Length);

                    stream.CopyTo(memStream);

                    if (memStream != null)
                    {
                        var assetBundle = AssetBundle.LoadFromMemory_Internal(memStream.ToArray(), 0);

                        if (assetBundle != null)
                        {
                            assetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;

                            bundle = assetBundle;

                            HasLoadedABundle = true;

                            return true;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads An Asset From The Previously Loaded AssetBundle
        /// </summary>
        /// <param name="str">The Internal Name Of The Asset Inside The AssetBundle</param>
        /// <returns>The Asset You Searched For, Null If No AssetBundle Was Previously Loaded</returns>
        internal T Load<T>(string str) where T : Object
        {
            if (HasLoadedABundle)
            {
                T Asset = bundle.LoadAsset(str, Il2CppType.Of<T>()).Cast<T>();

                Asset.hideFlags |= HideFlags.DontUnloadUnusedAsset;

                return Asset;
            }

            return null;
        }
    }
}
