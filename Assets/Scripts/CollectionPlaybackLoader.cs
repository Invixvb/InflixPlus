using System.Collections.Generic;
using Config;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CollectionPlaybackLoader : MonoBehaviour
{
    [Header("Collection Info")]
    [SerializeField] private TextMeshProUGUI currentItemsCountText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject volumeSlider;
    [SerializeField] private VideoPlayer mediaItemVideo;
    [SerializeField] private Image mediaItemImage;

    private SO_Collection _currentCollection;
    private readonly List<ScriptableObject> _currentMediaItemList = new();
    private int _currentIndex;

    /// <summary>
    /// Standard Unity Event.
    /// Load all the CollectionPlayback UI.
    /// </summary>
    private void OnEnable()
    {
        _currentCollection = StaticConfig.PublicConfig.currentCollection;

        // Set the UI text elements
        currentItemsCountText.text = $"Item {_currentCollection.mediaItem.currentMediaItemCount}/{_currentCollection.mediaItem.MediaItemCount}";
        titleText.text = _currentCollection.title;
        
        // Form 2 lists into 1
        _currentMediaItemList.AddRange(_currentCollection.mediaItem.photoItemsList);
        _currentMediaItemList.AddRange(_currentCollection.mediaItem.videoItemsList);

        // Get the index of the currentMediaItemCount, we do - 1 as this count is used for UI and we need to convert it into an index
        _currentIndex = _currentCollection.mediaItem.currentMediaItemCount - 1;

        // Determine if the currentIndex Item is a Photo or a Video and set UI elements accordingly
        if (_currentMediaItemList[_currentIndex].GetType() == typeof(SO_PhotoItem))
        {
            mediaItemImage.gameObject.SetActive(true);
            
            var photoItem = (SO_PhotoItem) _currentMediaItemList[_currentIndex];
            var currentHeaderSprite = Sprite.Create(photoItem.texture,
                new Rect(0, 0, photoItem.texture.width, photoItem.texture.height), Vector2.zero);
            
            mediaItemImage.sprite = currentHeaderSprite;
        }
        else if (_currentMediaItemList[_currentIndex].GetType() == typeof(SO_VideoItem))
        {
            var videoItem = (SO_VideoItem) _currentMediaItemList[_currentIndex];

            mediaItemImage.gameObject.SetActive(false);
            mediaItemVideo.clip = videoItem.videoClip;
            mediaItemVideo.Play();
        }

        _currentIndex++;
    }

    /// <summary>
    /// Used for the Button in the UI to load the next Item.
    /// </summary>
    public void LoadNextMediaItem()
    {
        // If we're in good range of the _currentIndex and the List count we execute, so we don't go out of bounds
        if (_currentIndex + 1 <= _currentMediaItemList.Count)
        {
            mediaItemVideo.Stop();
            
            // Determine if the currentIndex Item is a Photo or a Video and set UI elements accordingly
            if (_currentMediaItemList[_currentIndex].GetType() == typeof(SO_PhotoItem))
            {
                mediaItemImage.gameObject.SetActive(true);
                
                var photoItem = (SO_PhotoItem) _currentMediaItemList[_currentIndex];
                var currentHeaderSprite = Sprite.Create(photoItem.texture,
                    new Rect(0, 0, photoItem.texture.width, photoItem.texture.height), Vector2.zero);

                _currentIndex++;
            
                mediaItemImage.sprite = currentHeaderSprite;
            }
            else if (_currentMediaItemList[_currentIndex].GetType() == typeof(SO_VideoItem))
            {
                var videoItem = (SO_VideoItem) _currentMediaItemList[_currentIndex];

                _currentIndex++;

                mediaItemImage.gameObject.SetActive(false);
                mediaItemVideo.clip = videoItem.videoClip;
                mediaItemVideo.Play();
            }
            
            // Set the Text UI elements
            currentItemsCountText.text = $"Item {_currentIndex}/{_currentCollection.mediaItem.MediaItemCount}";
        }
    }
    
    /// <summary>
    /// Used for the Button in the UI to load the next Previous.
    /// </summary>
    public void LoadPreviousMediaItem()
    {
        // If we're in good range of the _currentIndex and the List count we execute, so we don't go out of bounds
        if (_currentIndex - 1 > 0)
        {
            _currentIndex--;
            mediaItemVideo.Stop();
            
            // Determine if the currentIndex Item is a Photo or a Video and set UI elements accordingly
            if (_currentMediaItemList[_currentIndex - 1].GetType() == typeof(SO_PhotoItem))
            {
                mediaItemImage.gameObject.SetActive(true);
                
                var photoItem = (SO_PhotoItem) _currentMediaItemList[_currentIndex - 1];
                var currentHeaderSprite = Sprite.Create(photoItem.texture,
                    new Rect(0, 0, photoItem.texture.width, photoItem.texture.height), Vector2.zero);

                mediaItemImage.sprite = currentHeaderSprite;
            }
            else if (_currentMediaItemList[_currentIndex - 1].GetType() == typeof(SO_VideoItem))
            {
                var videoItem = (SO_VideoItem) _currentMediaItemList[_currentIndex - 1];

                mediaItemImage.gameObject.SetActive(false);
                mediaItemVideo.clip = videoItem.videoClip;
                mediaItemVideo.Play();
            }
            
            // Set the Text UI elements
            currentItemsCountText.text = $"Item {_currentIndex}/{_currentCollection.mediaItem.MediaItemCount}";
        }
    }

    /// <summary>
    /// When clicked on the Volume button we toggle the slider with the inverse of the current activity.
    /// </summary>
    public void ToggleVolumeSlider()
    {
        volumeSlider.SetActive(!volumeSlider.activeSelf);
    }

    /// <summary>
    /// Standard Unity Event.
    /// When we click the Back button we reset the values so we don't get old values when loading it again.
    /// </summary>
    private void OnDisable()
    {
        StaticConfig.PublicConfig.currentCollection.mediaItem.currentMediaItemCount = _currentIndex;
        currentItemsCountText.text = "Item 13/99";
        titleText.text = "Title here";
        _currentMediaItemList.Clear();
    }
}
