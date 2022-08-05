using GameModes.GameplayMode.Levels.Basement.Objectives;
using MelonLoader;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay.ESP
{
    internal class KeyChams : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Key chams";

        internal Material chamsMaterial;

        internal bool appliedChams;

        private int _color;

        protected override void OnStart()
        {
            _color = Shader.PropertyToID("_Color");

            chamsMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };

            chamsMaterial.SetInt("_Cull", 0);
            chamsMaterial.SetInt("_ZWrite", 0);
            chamsMaterial.SetInt("_ZTest", 8);
            chamsMaterial.SetColor(_color, Color.cyan);

            appliedChams = false;
        }

        protected override void OnUpdate()
        {
            if (!appliedChams && Objects.Items.keys.Length > 0)
            {
                foreach (var key in Objects.Items.keys)
                    foreach (Renderer rend in key.GetComponentsInChildren<Renderer>())
                    {
                        Color color = GetKeyColor(key.keyType.ToString());

                        rend.material = chamsMaterial;
                        rend.material.SetColor(_color, color);

                        appliedChams = true;
                    }
            }
        }

        private Color GetKeyColor(string keyName)
        {
            Color color = Color.cyan;

            switch (keyName.ToUpper())
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
            }

            return color;
        }
    }
}
