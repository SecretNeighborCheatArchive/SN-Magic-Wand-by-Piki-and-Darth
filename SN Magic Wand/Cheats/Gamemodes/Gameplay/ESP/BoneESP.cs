using GameModes.Shared.Models.Bones;
using MelonLoader;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay.ESP
{
    internal class BoneESP : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal override string Name => "Bone ESP";


        protected override void OnGUI()
        {
            foreach (var plr in Players.AllPlayers)
                if (plr != null && plr != Players.localPlayer)
                {
                    var actor = plr.prop_Actor_0;

                    ActorModelBone[] bones = actor.GetComponents<ActorModelBone>();
                    
                    if (bones.Length > 0)
                        foreach (ActorModelBone bone in bones)
                        {
                            var boneW2s = Camera.main.WorldToScreenPoint(bone.prop_Vector3_0);
                            var centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);

                            ESPUtils.DrawLine(boneW2s, centerScreen, Color.cyan, 1.5f);

                            MelonLogger.Log(bone.name);
                        }
                }
        }
    }
}
