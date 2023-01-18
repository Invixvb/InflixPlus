using System.Collections.Generic;
using System.Linq;
using Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionScreenLoader : MonoBehaviour
{
    [Header("Collection Info")]
    [SerializeField] private Image maskedImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI totalItemsCountText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI currentItemsCountText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    [Header("Button Items")]
    [SerializeField] private List<GameObject> buttonItemList = new();
    
    private void OnEnable()
    {
        var currentCollection = StaticConfig.PublicConfig.currentCollection;

        var collectionImage = currentCollection.mediaItem.photoItemsList[0].texture;
        maskedImage.sprite = Sprite.Create(collectionImage,
            new Rect(0, 0, collectionImage.width, collectionImage.height), Vector2.zero);
        maskedImage.gameObject.GetComponent<RectTransform>().sizeDelta =
            new Vector2(collectionImage.width, collectionImage.height);

        titleText.text = currentCollection.title;
        dateText.text = currentCollection.year.ToString();
        totalItemsCountText.text = $"{currentCollection.mediaItem.MediaItemCount.ToString()} Items";
        progressSlider.value = currentCollection.mediaItem.currentMediaItemCount / currentCollection.mediaItem.MediaItemCount * 100;
        currentItemsCountText.text = $"{currentCollection.mediaItem.currentMediaItemCount}/{currentCollection.mediaItem.MediaItemCount} Items";
        descriptionText.text = currentCollection.description;

        for (var i = 0; i < currentCollection.mediaItem.MediaItemCount; i++)
        {
            var currentItem = buttonItemList[i];

            var childrenObjectList = currentItem.GetComponentsInChildren<Transform>(true)
                .Select(c => c.gameObject)
                .ToList();

            foreach (var child in childrenObjectList)
            {
                switch (child.name)
                {
                    case "ClippingImage":
                        var currentImage = currentCollection.mediaItem.photoItemsList[i].texture;
                        var currentHeaderSprite = Sprite.Create(currentImage,
                            new Rect(0, 0, currentImage.width, currentImage.height), Vector2.zero);
                        var targetImage = child.GetComponent<Image>();
                        targetImage.sprite = currentHeaderSprite;
                        targetImage.gameObject.GetComponent<RectTransform>().sizeDelta =
                            new Vector2(currentImage.width, currentImage.height);
                        break;
                }
            }
        }
    }
    
    public void AddFeaturedCollectionToWatchList()
    {
        StaticConfig.PublicConfig.currentCollection.addedToWatchList = !StaticConfig.PublicConfig.currentCollection.addedToWatchList;
    }

    public void GetSelectedMediaItem(int currentIndex)
    {
        StaticConfig.PublicConfig.currentCollection.mediaItem.currentMediaItemCount = currentIndex + 1;
    }

    private void OnDisable()
    {
        maskedImage.sprite = null;
        titleText.text = "Tile here";
        dateText.text = "Date";
        totalItemsCountText.text = "99 Items";
        progressSlider.value = 80;
        currentItemsCountText.text = "12/99 Items";
        descriptionText.text = "Description Here";
        
        foreach (var child in buttonItemList
                     .Select(currentItem => currentItem.GetComponentsInChildren<Transform>(true)
                     .Select(c => c.gameObject)
                     .ToList())
                     .SelectMany(childrenObjectList => childrenObjectList))
        {
            switch (child.name)
            {
                case "ClippingImage":
                    var targetImage = child.GetComponent<Image>();
                    targetImage.sprite = null;
                    break;
            }
        }
    }
}
