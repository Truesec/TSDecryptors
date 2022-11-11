using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using Truesec.Decryptors.Logging;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Download
{
    public class FileDownloader : IFileDownloader
    {
        private readonly IFileDownloaderCallback callback;
        private readonly ILogger logger;
        
        public FileDownloader(IFileDownloaderCallback callback, ILogger logger)
        {
            this.callback = callback;
            this.logger = logger;
        }

        public void DownloadFile(string url, string destination)
        {
            var thread = new Thread(() => {
                var client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(database_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(database_DownloadFileCompleted);
                try
                {
                    client.DownloadFileAsync(new Uri(url), destination);
                } catch(Exception)
                {
                    callback.DownloadCompleted(DownloadStatus.Failed);
                }
            });
            thread.Start();
        }

        void database_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            callback.DownloadProgession(e.BytesReceived, e.TotalBytesToReceive);
        }

        void database_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            callback.DownloadCompleted(e.Error == null ? DownloadStatus.Completed : DownloadStatus.Failed);
        }
    }
}
