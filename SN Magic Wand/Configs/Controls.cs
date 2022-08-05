using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Configs
{
    [Serializable]
    public class Controls
    {
        public static Controls current = new Controls();

        public KeyCode noclip = KeyCode.C;
        public KeyCode menu = KeyCode.Minus;
    }
}
