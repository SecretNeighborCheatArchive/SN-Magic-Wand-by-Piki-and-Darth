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
    internal class ThreeDPlayerBoxESP : ContinuousPlayerCheat
    {
        internal override string Name => "3D Box ESP";

        private Camera mainCam = Camera.main;

        protected override void OnStart(Player player)
        {
            mainCam = Camera.main;
        }

        protected override void OnGUI(Player player)
        {
            if (player != Players.localPlayer)
            {
                bool neighbour = player.prop_PlayerInfo_0.teamId == EnumPublicSealedvaALBEGADEEPZENECH9vUnique.NEIGHBOR;

                Color color = neighbour ? Color.red : Color.green;

                //ESPUtils.Draw3DBox(player.prop_Actor_0.GetComponent<Collider>().bounds, colour);

                Gizmos.color = color;
                foreach (var mf in player.prop_Actor_0.GetComponentsInChildren<MeshFilter>())
                {
                    Gizmos.matrix = mf.transform.localToWorldMatrix;
                    Mesh m = mf.sharedMesh;
                    Gizmos.DrawWireCube(m.bounds.center, m.bounds.size);
                }
            }
        }
    }
}
