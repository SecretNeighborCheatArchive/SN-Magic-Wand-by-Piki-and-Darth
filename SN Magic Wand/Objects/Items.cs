using GameModes.GameplayMode;
using GameModes.GameplayMode.Interactables.InventoryItems;
using GameModes.GameplayMode.Levels.Basement.Objectives;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SecretNeighbour.Objects
{
    internal static class Items
    {
        internal static RifleInventoryItem[] rifles = Array.Empty<RifleInventoryItem>();

        internal static KeyInventoryItem[] keys = Array.Empty<KeyInventoryItem>();

        private static float cacheTime = Time.time + 5f;

        /*public static IEnumerator GetItems()
        {
            rifles = Object.FindObjectsOfType<RifleInventoryItem>();

            yield return new WaitForSeconds(1f);

            var gcKeys = Object.FindObjectsOfType<KeyInventoryItem>();

            if (gcKeys != null)
                keys = gcKeys;

            yield return new WaitForSeconds(2f);

            MelonCoroutines.Start(GetItems());
        }*/

        internal static void Update()
        {
            if (Time.time >= cacheTime)
            {
                cacheTime = Time.time + 5f;

                rifles = Object.FindObjectsOfType<RifleInventoryItem>();
                keys = Object.FindObjectsOfType<KeyInventoryItem>();
            }
        }
    }
}
