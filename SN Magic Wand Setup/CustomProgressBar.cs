using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SN_Magic_Wand_Setup
{
    public class CustomProgressBar
    {
        public CustomProgressBar(Panel panel, PictureBox pic)
        {
            this.panel = panel;
            this.pic = pic;
            pic.Width = 0;
        }

        public float Percentage
        {
            get => percentage;
            set
            {
                if (value == percentage) return;
                percentage = value;
                pic.Width = (int)((value / 100f) * panel.Size.Width);
            }
        }
        private float percentage = 0;

        private readonly Panel panel;
        private readonly PictureBox pic;
    }
}
