using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HomeScreenLoader : MonoBehaviour
{
    [Header("Top Bar")]
    [SerializeField] private TextMeshProUGUI topBarAccountText;

    [Header("Featured Collection")]
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI locationText;
    [SerializeField] private TextMeshProUGUI mediaItemsText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image topImage1;
    [SerializeField] private Image topImage2;
    [SerializeField] private Image topImage3;

    [Header("Collection Rects")]
    [SerializeField] private List<GameObject> continueWatchingList = new();
    [SerializeField] private List<GameObject> recommendedList = new();
    [SerializeField] private List<GameObject> reWatchList = new(); //TODO: Set the bool back to false
    private Texture2D _currentHeaderImage;
    private string _currentTitleText;
    private string _currentItemText;
    private string _currentDateText;
    private int _currentSliderValue;

    [Header("Video Player")]
    [SerializeField] private VideoPlayer videoPlayer;
    private Texture2D _videoFrame;

    private SO_Collection _featuredCollection;

    /// <summary>
    /// Current Account being loaded into the UI.
    /// </summary>
    /// <param name="currentUser">User that has been clicked on.</param>
    public void LoadCurrentAccount(SO_Account currentUser)
    {
        // Set the top bar account text
        topBarAccountText.text = currentUser.userName;

        // Select a random collection from the user's collection list
        _featuredCollection = currentUser.collectionsList[Random.Range(0, currentUser.collectionsList.Count)];

        // Set the date, location, media item count, title, and description text
        dateText.text = _featuredCollection.year.ToString();
        locationText.text = _featuredCollection.location;
        mediaItemsText.text = $"{_featuredCollection.mediaItem.MediaItemCount.ToString()} Media Items";
        titleText.text = _featuredCollection.title;
        descriptionText.text = _featuredCollection.description;

        // Check if the collection has videoItems or photoItems
        var mediaItemsList = _featuredCollection.mediaItem.photoItemsList.Count <= 0
            ? _featuredCollection.mediaItem.videoItemsList.Cast<object>().ToList()
            : _featuredCollection.mediaItem.photoItemsList.Cast<object>().ToList();

        // Iterate through the media items list
        for (var i = 0; i < mediaItemsList.Count; i++)
        {
            var image = i switch
            {
                0 => topImage1,
                1 => topImage2,
                _ => topImage3
            };

            var mediaItem = mediaItemsList[i];

            // Check if the media item is a video or photo
            switch (mediaItem)
            {
                case SO_VideoItem videoItem:
                {
                    videoPlayer.clip = videoItem.videoClip;
                    videoPlayer.sendFrameReadyEvents = true;
                    videoPlayer.frameReady += OnNewFrame;

                    if (_videoFrame != null)
                        image.sprite = Sprite.Create(_videoFrame, new Rect(0, 0, _videoFrame.width, _videoFrame.height), Vector2.zero);

                    break;
                }
                case SO_PhotoItem photoItem:
                {
                    var photoTexture = photoItem.texture;
                    image.sprite = Sprite.Create(photoTexture, new Rect(0, 0, photoTexture.width, photoTexture.height), Vector2.zero);
                    image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(photoTexture.width, photoTexture.height);

                    break;
                }
            }
        }

        foreach (var collection in currentUser.collectionsList.Where(c => c.startedWatching))
        {
            var selectedCollections = new List<SO_Collection> {collection};
            
            for (var i = 0; i < selectedCollections.Count; i++)
            {
                var currentItem = continueWatchingList[i];

                var childrenObjectList = currentItem.GetComponentsInChildren<Transform>(true).Select(c => c.gameObject).ToList();

                foreach (var child in childrenObjectList)
                {
                    switch (child.name)
                    {
                        case "MaskedImage":
                            _currentHeaderImage = currentUser.collectionsList[i].mediaItem.photoItemsList[0].texture;
                            var currentHeaderSprite = Sprite.Create(_currentHeaderImage, new Rect(0, 0, _currentHeaderImage.width, _currentHeaderImage.height), Vector2.zero);
                            var targetImage = child.GetComponent<Image>();
                            targetImage.sprite = currentHeaderSprite;
                            targetImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(_currentHeaderImage.width, _currentHeaderImage.height);
                            break;
                    
                        case "TitleText":
                            _currentTitleText = currentUser.collectionsList[i].title;
                            child.GetComponent<TextMeshProUGUI>().text = _currentTitleText;
                            break;
                    
                        case "ItemText":
                            _currentItemText = $"Item {currentUser.collectionsList[i].mediaItem.currentMediaItemCount.ToString()}/{currentUser.collectionsList[i].mediaItem.MediaItemCount.ToString()}";
                            child.GetComponent<TextMeshProUGUI>().text = _currentItemText;
                            break;
                    
                        case "Text":
                            _currentDateText = currentUser.collectionsList[i].year.ToString();
                            child.GetComponent<TextMeshProUGUI>().text = _currentDateText;
                            break;
                    
                        case "ProgressSlider":
                            _currentSliderValue =
                                currentUser.collectionsList[i].mediaItem.currentMediaItemCount /
                                currentUser.collectionsList[i].mediaItem.MediaItemCount * 100;
                            child.GetComponent<Slider>().value = _currentSliderValue;
                            break;
                    }
                }
            }
        }

        for (var i = 0; i < currentUser.collectionsList.Count; i++)
        {
            var currentItem = recommendedList[i];

            var childrenObjectList = currentItem.GetComponentsInChildren<Transform>(true).Select(c => c.gameObject).ToList();

            foreach (var child in childrenObjectList)
            {
                switch (child.name)
                {
                    case "MaskedImage":
                        _currentHeaderImage = currentUser.collectionsList[i].mediaItem.photoItemsList[0].texture;
                        var currentHeaderSprite = Sprite.Create(_currentHeaderImage, new Rect(0, 0, _currentHeaderImage.width, _currentHeaderImage.height), Vector2.zero);
                        var targetImage = child.GetComponent<Image>();
                        targetImage.sprite = currentHeaderSprite;
                        targetImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(_currentHeaderImage.width, _currentHeaderImage.height);
                        break;
                    
                    case "TitleText":
                        _currentTitleText = currentUser.collectionsList[i].title;
                        child.GetComponent<TextMeshProUGUI>().text = _currentTitleText;
                        break;
                    
                    case "ItemText":
                        _currentItemText = $"Item {currentUser.collectionsList[i].mediaItem.currentMediaItemCount.ToString()}/{currentUser.collectionsList[i].mediaItem.MediaItemCount.ToString()}";
                        child.GetComponent<TextMeshProUGUI>().text = _currentItemText;
                        break;
                    
                    case "Text":
                        _currentDateText = currentUser.collectionsList[i].year.ToString();
                        child.GetComponent<TextMeshProUGUI>().text = _currentDateText;
                        break;
                    
                    case "ProgressSlider":
                        _currentSliderValue =
                            currentUser.collectionsList[i].mediaItem.currentMediaItemCount /
                            currentUser.collectionsList[i].mediaItem.MediaItemCount * 100;
                        child.GetComponent<Slider>().value = _currentSliderValue;
                        break;
                }
            }
        }

        foreach (var collection in currentUser.collectionsList.Where(c => c.seenCollection))
        {
            var selectedCollections = new List<SO_Collection> {collection};

            for (var i = 0; i < selectedCollections.Count; i++)
            {
                var currentItem = reWatchList[i];

                var childrenObjectList = currentItem.GetComponentsInChildren<Transform>(true).Select(c => c.gameObject).ToList();

                foreach (var child in childrenObjectList)
                {
                    switch (child.name)
                    {
                        case "MaskedImage":
                            _currentHeaderImage = currentUser.collectionsList[i].mediaItem.photoItemsList[0].texture;
                            var currentHeaderSprite = Sprite.Create(_currentHeaderImage, new Rect(0, 0, _currentHeaderImage.width, _currentHeaderImage.height), Vector2.zero);
                            var targetImage = child.GetComponent<Image>();
                            targetImage.sprite = currentHeaderSprite;
                            targetImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(_currentHeaderImage.width, _currentHeaderImage.height);
                            break;

                        case "TitleText":
                            _currentTitleText = currentUser.collectionsList[i].title;
                            child.GetComponent<TextMeshProUGUI>().text = _currentTitleText;
                            break;

                        case "ItemText":
                            _currentItemText = $"Item {currentUser.collectionsList[i].mediaItem.currentMediaItemCount.ToString()}/{currentUser.collectionsList[i].mediaItem.MediaItemCount.ToString()}";
                            child.GetComponent<TextMeshProUGUI>().text = _currentItemText;
                            break;

                        case "Text":
                            _currentDateText = currentUser.collectionsList[i].year.ToString();
                            child.GetComponent<TextMeshProUGUI>().text = _currentDateText;
                            break;

                        case "ProgressSlider":
                            _currentSliderValue = currentUser.collectionsList[i].mediaItem.currentMediaItemCount / currentUser.collectionsList[i].mediaItem.MediaItemCount * 100;
                            child.GetComponent<Slider>().value = _currentSliderValue;
                            break;
                    }
                }
            }
        }
    }
    
    //TODO: Actually open the CollectionScreen with the clicked Collection when we click a RectItem

    /// <summary>
    /// When we click the Account switching button we reset the values so we don't get old values.
    /// </summary>
    public void ResetValues()
    {
        topBarAccountText.text = "Person 1";
        _featuredCollection = null;
        dateText.text = "Date";
        locationText.text = "Location";
        mediaItemsText.text = "99 Media Items";
        titleText.text = "Title Here";
        descriptionText.text = "Description Here";
        topImage1.sprite = null;
        topImage2.sprite = null;
        topImage3.sprite = null;
        videoPlayer.clip = null;
        _currentHeaderImage = null;
        _currentTitleText = null;
        _currentItemText = null;
        _currentDateText = null;
        _currentSliderValue = 0;
    }
    
    /// <summary>
    /// When we click the "Watch Now" button in Unity we instantly play the Item we left on.
    /// </summary>
    public void WatchFeaturedCollection()
    {
        //TODO: Actually open the CollectionScreen with the selected featuredCollection
    }

    /// <summary>
    /// When we click the "Add to Watchlist" button in Unity we set the FeaturedCollection addedToWatchList bool to the inverse of what it was before.
    /// </summary>
    public void AddFeaturedCollectionToWatchList()
    {
        _featuredCollection.addedToWatchList = !_featuredCollection.addedToWatchList;
    }

    /// <summary>
    /// When a new frame is loaded from the VideoPlayer we get that texture of the loaded frame so we can use it as Thumbnail.
    /// </summary>
    /// <param name="source">The source VideoPlayer to use</param>
    /// <param name="frameIdx">The current Frame as ID.</param>
    private void OnNewFrame(VideoPlayer source, long frameIdx)
    {
        var renderTexture = source.texture as RenderTexture;

        // Replace the Texture width and height of the RenderTexture width and height, so it matches up
        if (renderTexture != null && (_videoFrame.width != renderTexture.width || _videoFrame.height != renderTexture.height))
            _videoFrame.Reinitialize(renderTexture.width, renderTexture.height);

        RenderTexture.active = renderTexture;
        
        // Get the Pixels of the RenderTexture and apply them to the Texture2D
        if (renderTexture != null)
            _videoFrame.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        _videoFrame.Apply();
        
        RenderTexture.active = null;

        // Remove the Event, prevention of Memory Leaks
        videoPlayer.sendFrameReadyEvents = false;
        videoPlayer.frameReady -= OnNewFrame;
    }
}
