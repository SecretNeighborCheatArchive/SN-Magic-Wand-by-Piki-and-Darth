using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SN_Magic_Wand_Setup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pb = new CustomProgressBar(panel3, pictureBox1);
        }

        public Download currentDownload;
        public CustomProgressBar pb;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dia = new OpenFileDialog();
            dia.Title = "Select the path of Secret Neighbour.exe";
            dia.Filter = "Secret Neighbour | Secret Neighbour.exe";
            if (dia.ShowDialog() != DialogResult.OK) return;
            textBox1.Text = dia.FileName;
        }

        private void DownloadProgressChanged(object sender, Download.DownloadProgressChangedArgs e)
        {
            pb.Percentage = e.Percentage;
        }

        private void Finished(object sender, EventArgs e)
        {
            pb.Percentage = 0;
            MessageBox.Show("A Magic Wand has been successfully added to your game!", "Installation completed");
            DownloadBtn.Enabled = true;
            button4.Enabled = true;
        }

        private void Failed(object sender, EventArgs e)
        {
            pb.Percentage = 0;
            MessageBox.Show("Something went wrong during the instalation.\nPlease check your internet connection and try again, otherwise contact support", "Installation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DownloadBtn.Enabled = true;
            button4.Enabled = true;
        }

        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Please specify the path of Secret Neighbor", "Installation failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DownloadBtn.Enabled = false;
            button4.Enabled = false;

            currentDownload = new Download();
            currentDownload.OnFinished += Finished;
            currentDownload.OnFailed += Failed;
            currentDownload.OnProgressChanged += DownloadProgressChanged;
            if (!Uninstall())
            {
                Failed(null, null);
                return;
            }
            currentDownload.StartDownloadAsync(Path.GetDirectoryName(textBox1.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Please specify the path of Secret Neighbor", "Uninstallation failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DownloadBtn.Enabled = false;
            button4.Enabled = false;

            if (Uninstall())
                MessageBox.Show("The Magic Wand has been successfully removed from your game!", "Uninstallation completed");
            else
                MessageBox.Show("Something went wrong during the uninstallation.\nPlease check your internet connection and try again, otherwise contact support", "Uninstallation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DownloadBtn.Enabled = true;
            button4.Enabled = true;
        }

        public bool Uninstall()
        {
            try
            {
                string paths;
                WebClient wc = new WebClient();
                paths = wc.DownloadString("https://raw.githubusercontent.com/PikiGames/snmagikalshit/main/PathsToRemove");
                RemovePaths(paths.Split('\n'));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void RemovePaths(string[] paths)
        {
            foreach (var p in paths)
            {
                if (p == string.Empty) continue;
                string a = Path.Combine(Path.GetDirectoryName(textBox1.Text), p);
                if (File.Exists(a)) File.Delete(a);
                if (Directory.Exists(a)) Directory.Delete(a, true);
            }
        }
    }
}
