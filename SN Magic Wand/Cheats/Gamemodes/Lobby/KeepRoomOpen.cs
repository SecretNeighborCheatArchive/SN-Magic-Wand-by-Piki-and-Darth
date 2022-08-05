using Photon.Pun;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes.Lobby
{
    /// <summary>
    /// Keep the room open even when there are 0 players in it.
    /// </summary>
    internal class KeepRoomOpen : SimpleCheat
    {
        internal override string Name => "Keep room open";

        protected override void OnExecute()
        {
            if (PhotonNetwork.CurrentRoom != null)       // Keep it random to lower the detection vectors.
                PhotonNetwork.CurrentRoom.EmptyRoomTtl = new Random(Environment.TickCount).Next(65535);
        }
    }
}
