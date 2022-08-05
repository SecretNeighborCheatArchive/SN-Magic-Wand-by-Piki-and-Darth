using GameModes.GameplayMode.Players;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay.ESP
{
    class Snaplines : ContinuousPlayerCheat
    {
        internal override string Name => "3D Snapline ESP";
        internal Material lineMat;

        protected override void OnInit()
        {
            lineMat = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
            };

            lineMat.SetInt("_SrcBlend", 5);
            lineMat.SetInt("_DstBlend", 10);
            lineMat.SetInt("_Cull", 0);
            lineMat.SetInt("_ZTest", 8);
            lineMat.SetInt("_ZWrite", 0);
            lineMat.SetColor("_Color", new Color32(30, 144, 255, 255));
        }

        protected override void OnUpdate(Player player)
        {
            
        }
    }
}
