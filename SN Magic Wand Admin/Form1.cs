using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SN_Magic_Wand_Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void CreateLicense()
        {
            var l = new License();
            l.name = "Unnamed";
            licenses.Add(l);
            var i = listBox1.Items.Add(l.name);
            listBox1.SelectedIndex = i;

            if (listBox1.Items.Count == 1)
            {
                panel1.Enabled = true;
                button2.Enabled = true;
            }
        }

        public void RemoveLicense(int index)
        {
            licenses.RemoveAt(index);
            listBox1.Items.RemoveAt(index);

            if (listBox1.Items.Count == 0)
            {
                panel1.Enabled = false;
                button2.Enabled = false;
            }
            else listBox1.SelectedIndex = 0;
        }

        public void SelectLicense(int index)
        {
            if (index >= licenses.Count || index < 0) return;
            var l = licenses[index];
            textBox1.Text = l.name;
            textBox2.Text = l.hwid;
            textBox3.Text = l.discordId;
        }

        public void UpdateLicense()
        {
            var l = licenses[listBox1.SelectedIndex];
            l.name = textBox1.Text;
            l.hwid = textBox2.Text;
            l.discordId = textBox3.Text;
            listBox1.Items[listBox1.SelectedIndex] = textBox1.Text;
        }
        
        public void Save()
        {
            byte[] encrypted = Program.Encrypt(ToString(), Program.key);
            string en = Convert.ToBase64String(encrypted);
            var existingFile = Program.client.Repository.Content.GetAllContents("SlidyDev", "snmagikalshit", "a").ConfigureAwait(false).GetAwaiter().GetResult();
            var a = Program.client.Repository.Content.UpdateFile("SlidyDev", "snmagikalshit", "a", new UpdateFileRequest("Update from admin console", en, existingFile.First().Sha)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        new public string ToString()
        {
            string result = string.Empty;
            foreach (var l in licenses)
            {
                result += l.ToString();
                result += "\n";
            }
            return result;
        }

        public void LoadLicenseList()
        {
            foreach (var s in decrypted.Split('\n'))
            {
                if (s == string.Empty) continue;
                License l = License.FromString(s);
                licenses.Add(l);
                listBox1.Items.Add(l.name);
            }
            if (licenses.Count > 0)
            {
                panel1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadLicenseList();
        }

        public List<License> licenses = new List<License>();
        public static string decrypted = string.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            CreateLicense();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectLicense(listBox1.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateLicense();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',') e.Handled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectLicense(listBox1.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemoveLicense(listBox1.SelectedIndex);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var dia = new OpenFileDialog();
            dia.Title = "Select the zipfile of the cheat you idiot";
            dia.Filter = "Zipped cheat | *.zip";
            if (dia.ShowDialog() != DialogResult.OK) return;
            ProcessCheatFile(dia.FileName);
        }

        private void ProcessCheatFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            int filesUploaded = 0;
            while (bytes.Length - 24000000 * filesUploaded > 0)
            {
                var bs = bytes.Skip(24000000 * filesUploaded).Take(24000000);
                StringBuilder sb = new StringBuilder();
                foreach (var b in bs)
                {
                    sb.Append((char)b);
                }
                Console.WriteLine("a");
                Program.client.Repository.Content.CreateFile("SlidyDev", "snmagikalshit", filesUploaded.ToString(), new CreateFileRequest("File uploaded from admin console", sb.ToString())).ConfigureAwait(false).GetAwaiter().GetResult();
                filesUploaded++;
            }
        }
    }
}
