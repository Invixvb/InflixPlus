using System.Threading.Tasks;
using Enums;
using Firebase.Storage;
using UnityEngine;

public static class MediaItemDownloader
{
    public static async Task DownloadMediaItem(MediaItemType mediaItemType, StorageReference storageReference)
    {
        // Create a temporary file path to download the file to
        var filePath = $@"D:\Documents\School\MBO Jaar 4\Meesterproef\InflixPlus\Assets\Resources\temp{mediaItemType}.png";

        // Download the file to the temporary path
        var downloadTask = storageReference.GetFileAsync(filePath);
        await downloadTask;

        // Check if the download was successful
        if (downloadTask.IsCompleted && !downloadTask.IsFaulted)
        {
            Debug.Log(filePath);
            // File has been successfully downloaded
            // Perform any necessary actions with the downloaded file
        }
        else
        {
            // Handle download failure
            Debug.LogError("Failed to download file from Firebase Storage");
        }
    }
}
