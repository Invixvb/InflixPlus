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
    /// Upload a media item to the cloud to storage it.
    /// </summary>
    /// <param name="mediaItem">The Item to Upload</param>
    public static async Task UploadMediaItem(object mediaItem)
    {
        try
        {
            byte[] mediaData;
            MediaItemType mediaItemType;
            switch (mediaItem)
            {
                case Texture2D texture:
                    // Get photo data
                    mediaData = texture.EncodeToPNG();
                    mediaItemType = MediaItemType.Photo;
                    break;
                case VideoClip videoClip:
                    // Get video clip data
                    mediaData = await File.ReadAllBytesAsync(videoClip.originalPath);
                    mediaItemType = MediaItemType.Video;
                    break;
                default:
                    Debug.LogError("Unsupported media item type");
                    return;
            }
            
            // Get the default Firebase Storage
            var storage = FirebaseStorage.DefaultInstance;
            
            // Create the filename based on a new Guid
            var newFileName = Guid.NewGuid();
            
            // Create a reference to the file in Firebase Storage
            var mediaRef = storage.GetReference($"/{mediaItemType}s/{newFileName}");

            // Create a storage reference to the file
            //var fileRef = mediaRef.Child(newFileName.ToString());

            // Upload the media data
            await mediaRef.PutBytesAsync(mediaData);
            
            // Log a message when the upload is complete
            Debug.Log("Media item uploaded successfully");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error uploading media item: {e.Message}");
        }
    }
}
