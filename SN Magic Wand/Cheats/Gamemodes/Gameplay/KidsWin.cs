using GameModes.GameplayMode.Levels.Basement;
using GameModes.GameplayMode.Levels.Modules;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class KidsWin : SimpleCheat
    {
        internal override string Name => "Kids Victory";

        protected override void OnExecute()
        {
            SendGlobalMessage(GameEndedMessage.Method_Public_Static_GameEndedMessage_EnumPublicSealedvaALBATIQU5vUnique_PDM_0(EnumPublicSealedvaALBATIQU5vUnique.BASEMENT_ENTERED));
        }
    }
}
