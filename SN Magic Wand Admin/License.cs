using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN_Magic_Wand_Admin
{
    public class License
    {
        new public string ToString()
        {
            return hwid + "," + discordId + "," + name;
        }

        public static License FromString(string s)
        {
            var l = new License();
            var split = s.Split(',');
            l.hwid = split[0];
            l.discordId = split[1];
            l.name = split[2];
            return l;
        }

        public string name = string.Empty;
        public string discordId = string.Empty;
        public string hwid = string.Empty;
    }
}
