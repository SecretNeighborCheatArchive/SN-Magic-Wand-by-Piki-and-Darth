using GameModes.GameplayMode.Levels.Modules;
using SecretNeighbour.Cheats.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes.Gameplay
{
    internal class QuestWin : SimpleCheat
    {
        internal override string Name => "? Ending";

        protected override void OnExecute()
        {
            SendGlobalMessage(GameEndedMessage.Method_Public_Static_GameEndedMessage_EnumPublicSealedvaALBATIQU5vUnique_PDM_0(EnumPublicSealedvaALBATIQU5vUnique.QUEST_COMPLETED));
        }
    }
}
