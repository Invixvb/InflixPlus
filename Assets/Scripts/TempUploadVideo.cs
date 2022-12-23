using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TempUploadVideo : MonoBehaviour
{
    public async void StartUpload()
    {
        var video = GetComponent<VideoPlayer>().clip;

        await MediaItemUploader.UploadMediaItem(new VideoMediaItem(video));
    }
}
