using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretNeighbour.Utils
{
    internal static class Extensions
    {
        internal static string xor(this string text, string key)
        {
            var result = new StringBuilder();
            for (int c = 0; c < text.Length; c += 2)
                result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));
            return result.ToString();
        }

        internal static string FromHex(this string hex)
        {
            string ascii = string.Empty;

            for (int i = 0; i < hex.Length; i += 2)
            {
                string hs = hex.Substring(i, 2);

                uint decval = Convert.ToUInt32(hs, 16);
                char character = Convert.ToChar(decval);
                ascii += character;
            }

            return ascii;
        }

        internal static string GetRealName(this Photon.Realtime.Room room)
        {
            if (room.CustomProperties["C0"].ToString() == "Quick")
                return "Quick Game";
            return room.CustomProperties["N"].ToString().FromHex().xor("Hh");
        }
    }
}
