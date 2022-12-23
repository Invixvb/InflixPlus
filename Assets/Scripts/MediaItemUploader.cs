using System;
using System.IO;
using UnityEngine;
using Firebase.Storage;
using System.Threading.Tasks;
using Enums;
using UnityEngine.Video;

public static class MediaItemUploader
{
    /// <summary>
    /// Upload a media item to the cloud to store it.
    /// </summary>
    /// <param name="mediaItem">The item to upload</param>
    public static async Task UploadMediaItem(object mediaItem)
    {
        try
        {
            // Get the default Firebase Storage
            var storage = FirebaseStorage.DefaultInstance;

            // Create the filename based on a new Guid
            var newFileName = Guid.NewGuid().ToString();

            // Create a reference to the file in Firebase Storage
            var mediaRef = storage.GetReference($"/{GetMediaItemType(mediaItem)}s/{newFileName}");

            // Upload the media data
            await mediaRef.PutBytesAsync(await GetMediaData(mediaItem));
        
            // Log a message when the upload is complete
            Debug.Log("Media item uploaded successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error uploading media item: {e.Message}");
        }
    }

    private static MediaItemType GetMediaItemType(object mediaItem)
    {
        switch (mediaItem)
        {
            case Texture2D:
                return MediaItemType.Photo;
            case VideoClip:
                return MediaItemType.Video;
            default:
                Debug.LogError("Unsupported media item type");
                return MediaItemType.Unsupported;
        }
    }
    
    private static async Task<byte[]> GetMediaData(object mediaItem)
    {
        switch (mediaItem)
        {
            case Texture2D texture:
                return texture.EncodeToPNG();
            case VideoClip videoClip:
                return await File.ReadAllBytesAsync(videoClip.originalPath);
            default:
                Debug.LogError("Couldn't get media item data");
                return Array.Empty<byte>();
        }
    }
}
