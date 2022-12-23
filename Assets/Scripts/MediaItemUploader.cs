using System;
using System.IO;
using System.Threading.Tasks;
using Enums;
using UnityEngine;
using Firebase.Storage;
using Interfaces;
using UnityEngine.Video;

public class PhotoMediaItem : IMediaItem
{
    private readonly Texture2D _texture;

    public PhotoMediaItem(Texture2D texture)
    {
        _texture = texture;
    }

    public MediaItemType Type => MediaItemType.Photo;

    public string FileName => Guid.NewGuid().ToString();

    public Task<byte[]> GetData()
    {
        return Task.FromResult(_texture.EncodeToPNG());
    }
}

public class VideoMediaItem : IMediaItem
{
    private readonly VideoClip _videoClip;

    public VideoMediaItem(VideoClip videoClip)
    {
        _videoClip = videoClip;
    }

    public MediaItemType Type => MediaItemType.Video;

    public string FileName => Guid.NewGuid().ToString();

    public async Task<byte[]> GetData()
    {
        return await File.ReadAllBytesAsync(_videoClip.originalPath);
    }
}

public static class MediaItemUploader
{
    /// <summary>
    /// Upload a media item to the cloud to store it.
    /// </summary>
    /// <param name="mediaItem">The item to upload</param>
    public static async Task UploadMediaItem(IMediaItem mediaItem)
    {
        try
        {
            // Get the default Firebase Storage
            var storage = FirebaseStorage.DefaultInstance;

            // Create a reference to the file in Firebase Storage
            var mediaRef = storage.GetReference($"/{mediaItem.Type}s/{mediaItem.FileName}");

            // Upload the media data
            await mediaRef.PutBytesAsync(await mediaItem.GetData());

            // Log a message when the upload is complete
            Debug.Log("Media item uploaded successfully");
        }
        catch (Exception e)
        {
            // If there's an error while upload the media item, we display it here
            Debug.LogError($"Error uploading media item: {e.Message}");
        }
    }
}
