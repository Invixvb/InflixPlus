using UnityEngine;
using UnityEngine.UI;

public class TempUploadImage : MonoBehaviour
{
    private Image _image;

    public void StartUpload()
    {
        _image = GetComponent<Image>();

        var imageSprite = _image.sprite;
        var imageTexture = imageSprite.texture;

        UploadMediaItem.Instance.StartUploadImage(imageTexture);
    }
}
