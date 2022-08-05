using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SN_Magic_Wand_Admin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Login().ConfigureAwait(false).GetAwaiter().GetResult();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
        }

        private static async Task Login()
        {
            client = new GitHubClient(new ProductHeaderValue("mg-admin"));
            client.Credentials = new Credentials("Yea, you can have all my old projects, but not my GitHub acc lmao");
            repos = Convert.FromBase64String((await client.Repository.Content.GetAllContents("SlidyDev", "snmagikalshit", "a")).First().Content);
            if (repos.Length != 0 && repos[repos.Length - 1] == 10) repos = repos.Take(repos.Count() - 1).ToArray();
            Form1.decrypted = repos.Length == 0 ? string.Empty : Decrypt(repos, key);
        }

        public static string Decrypt(byte[] buffer, string key)
        {
            byte[] iv = new byte[16];

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static byte[] Encrypt(string plainText, string key)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            return array;
        }

        public const string key = "BACEALgFAQcIC0owDAQOwD==";
        public static byte[] repos;
        public static GitHubClient client;
    }
}
