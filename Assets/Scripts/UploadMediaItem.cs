using System;
using System.Collections;
using Firebase.Storage;
using UnityEngine;

public class UploadMediaItem : MonoBehaviour
{
    #region Singleton pattern
    private static UploadMediaItem _instance;
    public static UploadMediaItem Instance
    {
        get 
        {
            if (!_instance)
                _instance = FindObjectOfType<UploadMediaItem>();
            return _instance;
        }
    }
    #endregion
    
    public void StartUploadImage(Texture2D image)
    {
        StartCoroutine(UploadImageCoroutine(image));
    }

    private IEnumerator UploadImageCoroutine(Texture2D image)
    {
        var storage = FirebaseStorage.DefaultInstance;
        var imageReference = storage.GetReference($"/images/{Guid.NewGuid()}");
        var bytes = image.EncodeToPNG();
        var uploadTask = imageReference.PutBytesAsync(bytes);

        yield return new WaitUntil(() => uploadTask.IsCompleted);

        if (uploadTask.Exception != null)
        {
            Debug.LogError($"Failed to Upload: {uploadTask.Exception}");
            yield break;
        }

        var getUrlTask = imageReference.GetDownloadUrlAsync();
        
        yield return new WaitUntil(() => getUrlTask.IsCompleted);

        if (getUrlTask.Exception != null)
        {
            Debug.LogError($"Failed to get download URL with {getUrlTask.Exception}");
        }
        
        Debug.Log($"Download from {getUrlTask.Result}");
    }
}
