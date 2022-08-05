using HoloNetwork.Messaging.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class SpawnButton
    {
        internal SpawnButton(GameObject item)
        {
            if (item == null)
                return;

            name = item.name.Replace("Item_", "");
            var objects = ObjectPublicOb2859Ob335257205827Unique.prop_ObjectPublicOb2859Ob335257205827Unique_0.field_Public_ObjectPublicDi2InGaHoDi2StHoObUnique_0.field_Private_Dictionary_2_Int32_GameObject_0;
            foreach (var ob in objects)
            {
                if (ob.Value == item)
                {
                    prefabId = ob.Key;
                    break;
                }
            }
        }

        internal void Spawn()
        {
            Transform cam = GameplayCheatController.instance.cam.transform;
            Ray ray = new Ray(cam.position, cam.forward);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, 3);
            Vector3 spawnPos = isHit ? hit.point : cam.position + cam.forward * 3;
            ObjectPublicDoBoObBoUnique.Method_Public_Static_Void_HoloNetGlobalMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0
            (
                SpawnNetObjectMessage.Method_Public_Static_SpawnNetObjectMessage_HoloNetObjectId_Int32_Vector3_Quaternion_0
                (
                    ObjectPublicOb2859Ob335257205827Unique.prop_ObjectPublicOb2859Ob335257205827Unique_0.field_Public_ObjectPublicDi2InGaHoDi2StHoObUnique_0.Method_Private_HoloNetObjectId_0(),
                    prefabId, spawnPos, default
                ),
                EnumPublicSealedvaOtAlSe5vSeUnique.All
            );
        }

        internal readonly string name;
        internal readonly int prefabId;
    }
}
