using SecretNeighbour.Cheats.Features;
using SecretNeighbour.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay.ESP
{
    internal class RifleChams : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Rifle Chams";

        internal bool appliedChams;

        private Material chamsMaterial;

        protected override void OnStart()
        {
            chamsMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };

            chamsMaterial.SetInt("_Cull", 0);
            chamsMaterial.SetInt("_ZWrite", 0);
            chamsMaterial.SetInt("_ZTest", 8);
            chamsMaterial.SetColor("_Color", Color.blue);

            // When we load into a new game we want to re-apply chams.
            appliedChams = false;
        }

        protected override void OnUpdate()
        {
            if (!appliedChams && Objects.Items.rifles.Length > 0)
            {
                foreach (var rifle in Objects.Items.rifles)
                    foreach (Renderer renderer in rifle.GetComponentsInChildren<Renderer>())
                    {
                        //renderer.material = chamsMaterial;
                        renderer.material.shader = CheatUtils.chamsOutline;

                        renderer.material.SetColor("_FirstOutlineColor", Color.red);
                        renderer.material.SetFloat("_FirstOutlineWidth", 0.02f);

                        renderer.material.SetColor("_SecondOutlineColor", Color.blue);
                        renderer.material.SetFloat("_SecondOutlineWidth", 0.0025f);

                        appliedChams = true;
                    }
            }
        }
    }
}
