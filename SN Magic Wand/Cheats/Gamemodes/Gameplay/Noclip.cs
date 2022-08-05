using GameModes.GameplayMode.Actors;
using SecretNeighbour.Cheats.Features;
using SecretNeighbour.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class Noclip : ContinuousCheat
    {
        internal override string Name => "Noclip";
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY;

        internal float speed => CheatConfig.current.noclipSpeed;

        private Actor CurrentActor => Players.localPlayer.prop_Actor_0;


        protected override void OnEnabledToggle()
        {
            CurrentActor.GetComponent<Collider>().enabled = !Enabled;
        }

        protected override void OnUpdate()
        {
            switch (CheatConfig.current.noclipMode)
            {
                case NoclipMode.Flat: DoFlat(); break;
                case NoclipMode.CameraFollow: DoCameraFollow(); break;
            }
        }

        private void DoFlat()
        {
            Transform t = CurrentActor.transform;
            t.position += (t.forward * Input.GetAxisRaw("Vertical") + t.right * Input.GetAxisRaw("Horizontal")) * Time.deltaTime * speed;

            bool up = Input.GetKey(KeyCode.Space);
            bool down = Input.GetKey(KeyCode.LeftControl);

            if ((up || down) && up != down)
                t.position += t.up * (down ? -1 : 1) * Time.deltaTime * speed;
        }

        private void DoCameraFollow()
        {
            Transform cam = Camera.main.transform;
            Transform t = CurrentActor.transform;
            t.position += (cam.forward * Input.GetAxisRaw("Vertical") + t.right * Input.GetAxisRaw("Horizontal")) * Time.deltaTime * speed;
        }
    }
}
