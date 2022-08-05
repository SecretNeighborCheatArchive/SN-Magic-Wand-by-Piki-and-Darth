using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SN_Magic_Wand_Setup
{
    public class Download
    {
        public void StartDownloadAsync(string unzipPath)
        {
            if (isDownloading) return;
            unzip = unzipPath;
            GetReady();
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += InfoDownloaded;
            try
            {
                wc.DownloadStringAsync(new Uri(path + "DownloadOrder"));
            }
            catch 
            {
                Fail();
            }
        }

        private void GetReady()
        {
            isDownloading = true;
            filesDownloaded = 0;
        }

        private void Fail()
        {
            isDownloading = false;
            OnFailed?.Invoke(this, default);
        }

        private void InfoDownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            Console.WriteLine(e.Result.Split('\n').Length);
            foreach (var s in e.Result.Split('\n'))
            {
                if (s == string.Empty) continue;
                filesToDownload.Add(s);
            }
            DownloadFile();
        }

        private void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            Unzip(tempFile, unzip);
            File.Delete(tempFile);

            filesDownloaded++;

            DownloadFile();
        }

        private void DownloadFile()
        {
            if (filesDownloaded == filesToDownload.Count)
            {
                Finish();
                return;
            }
            tempFile = Path.GetTempFileName();
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += OnProgressChange;
            wc.DownloadFileCompleted += FileDownloaded;
            wc.DownloadFileAsync(new Uri(filesToDownload[filesDownloaded]), tempFile);
        }

        private void OnProgressChange(object sender, DownloadProgressChangedEventArgs e)
        {
            var arg = new DownloadProgressChangedArgs((100 * filesDownloaded + e.ProgressPercentage) / filesToDownload.Count);
            OnProgressChanged?.Invoke(this, arg);
        }

        private void Finish()
        {
            isDownloading = false;
            OnFinished?.Invoke(this, default);
        }

        private void Unzip(string zipFileName, string dest)
        {
            var str = new FileStream(zipFileName, FileMode.Open);
            var archive = new ZipArchive(str);
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(dest, file.FullName);
                string directory = Path.GetDirectoryName(completeFileName);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (file.Name != "")
                    file.ExtractToFile(completeFileName, true);
            }
            str.Close();
        }

        public event EventHandler<DownloadProgressChangedArgs> OnProgressChanged;
        public event EventHandler OnFinished;
        public event EventHandler OnFailed;
        private string unzip;
        private string tempFile;
        private int filesDownloaded = 0;
        private bool isDownloading = false;
        private List<string> filesToDownload = new List<string>();
        private const string path = "https://raw.githubusercontent.com/PikiGames/snmagikalshit/main/";

        public class DownloadProgressChangedArgs : EventArgs
        {
            public DownloadProgressChangedArgs(float percentage)
            {
                Percentage = percentage;
            }

            public float Percentage { get; private set; }
        }
    }
}
