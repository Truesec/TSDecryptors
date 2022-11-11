namespace Truesec.Decryptors.Interfaces
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string destination);
    }

    public enum DownloadStatus
    {
        NotStarted = 0,
        Started = 1,
        Completed = 2,
        Validated = 3,
        Failed = 4,
        InvalidValidation = 5
    }

    public interface IFileDownloaderCallback
    {
        void DownloadProgession(long bytesRead, long totalBytes);
        void ResetProgression();
        void DownloadCompleted(DownloadStatus status);
    }
}
