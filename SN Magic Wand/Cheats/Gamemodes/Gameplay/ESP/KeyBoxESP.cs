using GameModes.GameplayMode.Levels.Basement.Objectives;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay.ESP
{
    internal class KeyBoxESP : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Key box ESP";

        private Camera mainCam = Camera.main;

        private Dictionary<KeyInventoryItem, Collider> colliders = new Dictionary<KeyInventoryItem, Collider>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnStart()
        {
            mainCam = Camera.main;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void OnGUI()
        {
            foreach (KeyInventoryItem key in Objects.Items.keys)
            {
                if (!colliders.ContainsKey(key))
                    colliders.Add(key, key.GetComponent<Collider>());

                var keyName = key.keyType.ToString();

                Color color = Color.cyan;

                switch (keyName)
                {
                    case "RED":
                        color = Color.red;
                        break;
                    case "YELLOW":
                        color = Color.yellow;
                        break;
                    case "BLUE":
                        color = Color.blue;
                        break;
                    default:
                        continue;
                }

                //ESPUtils.Draw3DBox(colliders[key].bounds, color);
            }
        }
    }
}
