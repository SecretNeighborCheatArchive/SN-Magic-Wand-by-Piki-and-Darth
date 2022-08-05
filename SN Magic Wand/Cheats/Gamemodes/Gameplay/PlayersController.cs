using AppControllers;
using GameModes.GameplayMode;
using GameModes.GameplayMode.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class PlayersController
    {
        internal PlayersController()
        {
            if (Main.CurrentGamemode != EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.GAMEPLAY) throw new Exception("Gamemode must be Gameplay to create a Players Controller");
            var plrs = Object.FindObjectsOfType<Player>();
            _players = plrs.ToList();
            localPlayer = plrs.FirstOrDefault(p => p.prop_HoloNetObject_0.IsLocal);
        }

        internal void SelectPlayer(Player plr)
        {
            selectedPlayer = plr;
        }

        internal void Update()
        {
            if (_players.RemoveAll(x => x == null) > 0)
            {
                EventHandler handler = OnPlayerLeft;
                handler?.Invoke(this, null);
            }
        }

        private List<Player> _players;

        internal IReadOnlyList<Player> AllPlayers => _players;

        internal event EventHandler OnPlayerLeft;
        internal Player selectedPlayer;
        internal Player localPlayer;
        internal Player neighbor;
    }
}
