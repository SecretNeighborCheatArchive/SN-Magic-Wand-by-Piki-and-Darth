using BackEnd;
using GameModes.GameplayMode.Actors.Shared;
using GameModes.GameplayMode.Players;
using GameModes.LobbyMode.LobbyPlayers;
using GameModes.LobbyMode.LobbyPlayers.Messages;
using GameModes.Shared.Loadouts.Classes;
using HoloNetwork.Messaging.Implementations;
using HoloNetwork.Players;
using Photon.Pun;
using System.Reflection;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
using System.Threading.Tasks;
using GameModes.GameplayMode.Actors;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json;

namespace SecretNeighbour.Cheats
{
    static class CheatUtils
    {
        static Material tracerMat = new Material(Shader.Find("Hidden/Internal-Colored"));
        internal static Shader chamsOutline;
        internal static Shader wireframe;

        internal static void Update()
        {
            if (Time.time % 8 == 0)
                mainCam = Camera.main;
        }

        internal static void SendMessage(HoloNetGlobalMessage msg, EnumPublicSealedvaOtAlSe5vSeUnique target = EnumPublicSealedvaOtAlSe5vSeUnique.All)
        {
            ObjectPublicDoBoObBoUnique.Method_Public_Static_Void_HoloNetGlobalMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0(msg, target);
        }

        internal static void SetPrivateField(this object obj, string name, object value)
        {
            Type type_ = obj.GetType();
            FieldInfo fieldInfo_ = type_.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo_.SetValue(obj, value);
        }

        internal static void SetPublicField(this object obj, string name, object value)
        {
            Type type_ = obj.GetType(); 
            FieldInfo fieldInfo_ = type_.GetField(name, BindingFlags.Instance | BindingFlags.Public);
            fieldInfo_.SetValue(obj, value);
        }

        internal static void SendMessage(HoloNetGlobalMessage msg, HoloNetPlayer target)
        {
            ObjectPublicDoBoObBoUnique.Method_Public_Static_Void_HoloNetGlobalMessage_HoloNetPlayer_PDM_0(msg, target);
        }

        internal static void ChangePlayerInfo(this LobbyPlayer player, PlayerInfo info, ActorClassLoadout explorer, ActorClassLoadout neighbour)
        {
            // TODO: LobbyProtection.allowOneChange = true;

            player.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0
            (
                LobbyPlayerSyncInfoMessage.Method_Public_Static_LobbyPlayerSyncInfoMessage_PlayerInfo_ActorClassLoadout_ActorClassLoadout_PDM_0
                (
                    info, explorer,
                    neighbour
                ),
                EnumPublicSealedvaOtAlSe5vSeUnique.All
            );
        }

        internal static RaycastHit? Raycast(Vector3 position, Vector3 direction)
        {
            if (Physics.Raycast(new Ray(position, direction), out RaycastHit hit))
                return hit;

            return null;
        }

        internal static void DrawTracer(Vector3 start, Vector3 end, Color colour, float tracerWidth = 0.05f, float lifeTime = 0.7f)
        {
            var go = new GameObject();
            go.transform.position = start;

            var lRenderer = go.AddComponent<LineRenderer>();
            lRenderer.material = tracerMat;

            var mat = lRenderer.material;

            mat.SetInt("_Cull", 0);
            mat.SetInt("_ZWrite", 0);
            mat.SetInt("_ZTest", 8);
            mat.SetColor("_Color", colour);

            lRenderer.SetWidth(tracerWidth, tracerWidth);
            lRenderer.SetPosition(0, start);
            lRenderer.SetPosition(1, end);

            Object.Destroy(go, lifeTime);
        }

        internal static void GhostKill(Player plr)
        {
            foreach (HoloNetPlayer holoNetPlayer in HoloNetPlayer.prop_List_1_HoloNetPlayer_0)
            {
                if (holoNetPlayer.uniqueId._value != plr.prop_HoloNetObject_0.prop_HoloNetPlayer_0.uniqueId._value)
                    plr.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_HoloNetPlayer_0(DecideDeathMessage.Method_Public_Static_DecideDeathMessage_EnumPublicSealedvaNEKNBARE5vUnique_0(EnumPublicSealedvaNEKNBARE5vUnique.NEIGHBOR_KILL), holoNetPlayer);
            }
        }

        internal static void Kill(Player plr)
        {
            foreach (HoloNetPlayer holoNetPlayer in HoloNetPlayer.prop_List_1_HoloNetPlayer_0)
                plr.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_HoloNetPlayer_0(DecideDeathMessage.Method_Public_Static_DecideDeathMessage_EnumPublicSealedvaNEKNBARE5vUnique_0(EnumPublicSealedvaNEKNBARE5vUnique.NEIGHBOR_KILL), holoNetPlayer);
        }

        private static Camera mainCam = Camera.main;
        internal static Task WorldToScreen(Actor curActor, out Vector2 w2s)
        {
            w2s = mainCam.WorldToScreenPoint(curActor.transform.position);
            w2s.y = Screen.height - (w2s.y + 1f);

            return Task.CompletedTask;
        }

        internal static void ChangeRoomSettings(string name, string password, string locale)
        {
            Photon.Realtime.Room currentRoom = PhotonNetwork.CurrentRoom;
            ExitGames.Client.Photon.Hashtable customProperties = currentRoom.CustomProperties;

            customProperties["C2"] = name;
            customProperties["C3"] = password;
            customProperties["C1"] = locale;
            customProperties["N"] = name;
            customProperties["P"] = password;

            currentRoom.SetCustomProperties(customProperties, null, null);
        }

        internal static void Buff(this Player player, EnumPublicSealedvaSTCAGLCADITODIKNSLUnique buff)
        {
            player.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0
            (
                ApplyBuffByIdMessage.Method_Public_Static_ApplyBuffByIdMessage_EnumPublicSealedvaSTCAGLCADITODIKNSLUnique_ArrayOf_Object_0(buff, null),
                EnumPublicSealedvaOtAlSe5vSeUnique.All
            );
        }

        internal static void Debuff(this Player player, EnumPublicSealedvaSTCAGLCADITODIKNSLUnique buff)
        {
            player.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0
            (
                VanishBuffMessage.Method_Public_Static_VanishBuffMessage_EnumPublicSealedvaSTCAGLCADITODIKNSLUnique_0(buff),
                EnumPublicSealedvaOtAlSe5vSeUnique.All
            );
        }
        internal static void Teleport(this Player player, Vector3 position)
        {
            player.prop_HoloNetObject_0.Method_Public_Void_HoloNetObjectMessage_EnumPublicSealedvaOtAlSe5vSeUnique_0(ActorTeleportPositionMessage.Method_Public_Static_ActorTeleportPositionMessage_Vector3_0(position), EnumPublicSealedvaOtAlSe5vSeUnique.All);
        }
    }
}
