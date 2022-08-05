using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Cheats.Gamemodes
{
    public interface IGameMode
    {
        void Start();
        void Update();
        void FixedUpdate();
        void End();
        void OnGUI();

        EnumPublicSealedvaNOGALOMEPRGAMASHCRUnique Gamemode { get; }
    }
}
