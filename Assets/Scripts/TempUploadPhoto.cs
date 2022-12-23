using UnityEngine;
using UnityEngine.UI;

public class TempUploadPhoto : MonoBehaviour
{
    public async void StartUpload()
    {
        var image = GetComponent<Image>();

        var imageSprite = image.sprite;
        var imageTexture = imageSprite.texture;

        await MediaItemUploader.UploadMediaItem(new PhotoMediaItem(imageTexture));
    }
}
