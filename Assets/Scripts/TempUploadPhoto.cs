using UnityEngine;
using UnityEngine.UI;

public class TempUploadPhoto : MonoBehaviour
{
    public async void StartUpload()
    {
        var image = GetComponent<Image>().sprite.texture;

        await MediaItemUploader.UploadMediaItem(new MediaItemUploader.PhotoMediaItem(image));
    }
}
