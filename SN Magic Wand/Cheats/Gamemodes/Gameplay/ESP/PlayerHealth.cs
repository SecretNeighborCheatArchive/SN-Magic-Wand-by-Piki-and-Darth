using GameModes.GameplayMode.Actors.Shared.Configuration;
using GameModes.GameplayMode.Players;
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
    internal class PlayerHealth : ContinuousPlayerCheat
    {
        internal override string Name => "Player health ESP";

        private Camera mainCam = Camera.main;

        private Dictionary<Player, PlayerHealthSnapshot> health = new Dictionary<Player, PlayerHealthSnapshot>();

        protected override void OnStart(Player player)
        {
            mainCam = Camera.main;
        }

        protected override void OnGUI(Player player)
        {
            // Only paint once per frame, save FPS.
            if (Event.current.type != EventType.Repaint)
                return;

            if (player != Players.localPlayer)
            {
                if (!health.ContainsKey(player))
                    health.Add(player, player.GetComponent<PlayerHealthSnapshot>());

                Vector2 w2s = mainCam.WorldToScreenPoint(player.prop_Actor_0.transform.position);
                w2s.y = Screen.height - (w2s.y + 1f);

                ESPUtils.DrawHealth1(w2s, health[player].health, true);
            }
        }
    }
}
