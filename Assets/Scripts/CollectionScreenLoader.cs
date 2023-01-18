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
    
    /// <summary>
    /// Standard Unity Event.
    /// Used to load all the UI elements in the CollectionScreen UI.
    /// </summary>
    private void OnEnable()
    {
        var currentCollection = StaticConfig.PublicConfig.currentCollection;

        // Set the HeaderImage of the CollectionScreen
        var collectionImage = currentCollection.mediaItem.photoItemsList[0].texture;
        maskedImage.sprite = Sprite.Create(collectionImage,
            new Rect(0, 0, collectionImage.width, collectionImage.height), Vector2.zero);
        maskedImage.gameObject.GetComponent<RectTransform>().sizeDelta =
            new Vector2(collectionImage.width, collectionImage.height);

        // Set all the UI text elements of the CollectionScreen
        titleText.text = currentCollection.title;
        dateText.text = currentCollection.year.ToString();
        totalItemsCountText.text = $"{currentCollection.mediaItem.MediaItemCount.ToString()} Items";
        progressSlider.value = currentCollection.mediaItem.currentMediaItemCount / currentCollection.mediaItem.MediaItemCount * 100;
        currentItemsCountText.text = $"{currentCollection.mediaItem.currentMediaItemCount}/{currentCollection.mediaItem.MediaItemCount} Items";
        descriptionText.text = currentCollection.description;

        // For every MediaItem
        for (var i = 0; i < currentCollection.mediaItem.MediaItemCount; i++)
        {
            var currentItem = buttonItemList[i];

            // Get all the Children GOs of the currentItem in the buttonItemList
            var childrenObjectList = currentItem.GetComponentsInChildren<Transform>(true)
                .Select(c => c.gameObject)
                .ToList();

            // For every Child in the list we check if the child title is any of the selection, and then we react accordingly
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
    
    /// <summary>
    /// Set the bool of the CurrentCollection to the inverse of its current status.
    /// </summary>
    public void AddCurrentCollectionToWatchList()
    {
        StaticConfig.PublicConfig.currentCollection.addedToWatchList = !StaticConfig.PublicConfig.currentCollection.addedToWatchList;
    }

    /// <summary>
    /// Get the selected MediaItem in the CollectionScreen.
    /// </summary>
    /// <param name="currentIndex">The index of the ItemButton of CollectionScreen</param>
    public void GetSelectedMediaItem(int currentIndex)
    {
        // + 1 as the currentMediaItemCount is used for UI elements and those don't start at 0
        StaticConfig.PublicConfig.currentCollection.mediaItem.currentMediaItemCount = currentIndex + 1;
    }

    /// <summary>
    /// Standard Unity Event.
    /// When we disable the screen we reset the values so we don't get old values next time loading the CollectionScreen.
    /// </summary>
    private void OnDisable()
    {
        maskedImage.sprite = null;
        titleText.text = "Tile here";
        dateText.text = "Date";
        totalItemsCountText.text = "99 Items";
        progressSlider.value = 80;
        currentItemsCountText.text = "12/99 Items";
        descriptionText.text = "Description Here";
        
        // Get all the GOs of the children and set it to the list and then iterate through the list to set the ClippingImage to white
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
