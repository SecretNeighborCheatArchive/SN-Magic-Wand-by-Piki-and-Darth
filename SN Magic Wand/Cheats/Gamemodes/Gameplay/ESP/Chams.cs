using GameModes.GameplayMode.Actors;
using GameModes.GameplayMode.Players;
using MelonLoader;
using SecretNeighbour.Cheats.Features;
using SecretNeighbour.Cheats.Gamemodes;
using SecretNeighbour.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class Chams : ContinuousPlayerCheat
    {
        internal override string Name => "X-Ray";


        private readonly Color[] chamColours = new Color[]
        {
            // Final colour must match the first colour for a smooth change.
            Color.blue, Color.green, Color.magenta, Color.red, Color.yellow, Color.blue
        };

        internal enum ChamsType
        {
            FLAT,
            XRAY
        }

        internal ChamsType curChamType = ChamsType.XRAY;

        /// <summary>
        /// Current cham colour. Must start with the first colour in chamColours.
        /// </summary>
        private Color curChamsColour = Color.blue;

        /// <summary>
        /// The amount of time it takes to Interpolate through every colour.
        /// </summary>
        private readonly float chamLerpDuration = 15f;

        /// <summary>
        /// Material that you can see through walls with a custom colour.
        /// </summary>
        private Material chamsMaterial;

        /// <summary>
        /// Material that cannot be seen through walls and is permanently pink.
        /// Objects using this Material are transparent.
        /// </summary>
        private Material chamsErrorMaterial;

        private Material chamsFlat;

        /// <summary>
        /// If true, then the shader used will be visible through walls, and it's colour will lerp between
        /// the colours inside chamColours.
        /// </summary>
        internal bool throughWalls = true;

        internal bool outline = true;

        /// <summary>
        /// Save FPS, Unity will hash property names so we'll just get the ID so we don't need to have them hashed in the future.
        /// </summary>
        private int _Color = 0;

        protected override void OnEnabledToggle(Player player, bool enabled)
        {
            if (!enabled || enabled && !throughWalls)
                MelonCoroutines.Stop(LerpColors());
            else if (!throughWalls)
                MelonCoroutines.Start(LerpColors());
        }

        protected override void OnInit()
        {
            MelonCoroutines.Start(LerpColors());
        }

        protected override void OnStart(Player player)
        {
            chamsMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };

            chamsFlat = new Material(Shader.Find("Standard"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };

            /*chamsErrorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"))
            {
                hideFlags = HideFlags.NotEditable | HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };*/

            if (_Color == 0)
                _Color = Shader.PropertyToID("_Color");

            chamsMaterial.SetInt("_SrcBlend", 5);
            chamsMaterial.SetInt("_DstBlend", 10);
            chamsMaterial.SetInt("_Cull", 0);
            chamsMaterial.SetInt("_ZTest", 8);
            chamsMaterial.SetInt("_ZWrite", 0);
            
            chamsFlat.SetFloat("_Glossiness", 1f);
            chamsFlat.SetFloat("_OcclusionStrength", 0f);
            chamsFlat.SetColor("__EmissionColor", curChamsColour);
            chamsFlat.SetInt("_ZWrite", 0);
            chamsFlat.SetInt("_SrcBlend", 5);
            chamsFlat.SetInt("_DstBlend", 10);
        }

        protected override void OnUpdate(Player player)
        {
            if (!CheatConfig.current.chams)
                return;

            chamsMaterial.SetColor(_Color, curChamsColour);
            chamsFlat.SetColor(_Color, curChamsColour);

            GameObject go = player.gameObject;

            foreach (SkinnedMeshRenderer renderer in go.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (player == Players.localPlayer && curChamType == ChamsType.XRAY)
                    continue;

                if (!outline && (renderer.material != chamsFlat || renderer.material != chamsMaterial))
                    renderer.material = (curChamType == ChamsType.XRAY) ? chamsMaterial : chamsFlat;
                else if (renderer.material.shader != CheatUtils.chamsOutline)
                {
                    renderer.material.shader = CheatUtils.chamsOutline;

                    renderer.material.SetColor("_FirstOutlineColor", Color.red);
                    renderer.material.SetFloat("_FirstOutlineWidth", 0.02f);

                    renderer.material.SetColor("_SecondOutlineColor", curChamsColour);
                    renderer.material.SetFloat("_SecondOutlineWidth", 0.0025f);
                }
            }
        }

        private IEnumerator LerpColors()
        {
            for (;;)
            {
                if (chamColours.Length > 0)
                {
                    // Split the time between the color quantities.
                    float dividedDuration = chamLerpDuration / chamColours.Length;

                    for (int i = 0; i < chamColours.Length - 1; i++)
                    {
                        float t = 0.0f;

                        while (t < (1.0f + Mathf.Epsilon))
                        {
                            curChamsColour = Color.Lerp(chamColours[i], chamColours[i + 1], t);

                            t += Time.deltaTime / dividedDuration;

                            yield return null;
                        }

                        // Since it is posible that t does not reach 1.0, force it at the end.
                        curChamsColour = Color.Lerp(chamColours[i], chamColours[i + 1], 1.0f);
                    }

                }
                else
                    yield return null; // Do nothing if there are no colors.
            }
        }
    }
}
