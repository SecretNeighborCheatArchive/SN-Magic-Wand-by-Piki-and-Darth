using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Menu
{
    static class MenuUtils
    {
        private static readonly List<int> previousIDs = new List<int>(); 
        private static readonly Random rnd = new Random();


        internal static int GenerateWindowIDUnique()
        {
            int rand = rnd.Next(2048);

            if (previousIDs.Contains(rand))
                return GenerateWindowIDUnique();
            else
                previousIDs.Add(rand);

            return rand;
        }
    }
}
