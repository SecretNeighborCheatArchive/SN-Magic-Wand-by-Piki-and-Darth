using Photon.Pun;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes.Lobby
{
    internal class CrashLobby : ContinuousCheat
    {
        internal override EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode => EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique.LOBBY;

        internal override string Name => "Crash a lobby";
        
        protected override void OnEnabledToggle()
        {
            // I don't know which one we're supposed to change, so I'll change them both.
            PhotonNetwork.IsMessageQueueRunning = Enabled;
            PhotonNetwork.isMessageQueueRunning = Enabled;
        }
    }
}
