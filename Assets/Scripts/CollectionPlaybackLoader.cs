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
    [SerializeField] private int _currentIndex;

    private void OnEnable()
    {
        _currentCollection = StaticConfig.PublicConfig.currentCollection;

        currentItemsCountText.text = $"Item {_currentCollection.mediaItem.currentMediaItemCount}/{_currentCollection.mediaItem.MediaItemCount}";
        titleText.text = _currentCollection.title;
        
        _currentMediaItemList.AddRange(_currentCollection.mediaItem.photoItemsList);
        _currentMediaItemList.AddRange(_currentCollection.mediaItem.videoItemsList);

        _currentIndex = _currentCollection.mediaItem.currentMediaItemCount - 1;

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

    public void LoadNextMediaItem()
    {
        if (_currentIndex + 1 <= _currentMediaItemList.Count)
        {
            mediaItemVideo.Stop();
            
            if (_currentMediaItemList[_currentIndex].GetType() == typeof(SO_PhotoItem))
            {
                mediaItemImage.gameObject.SetActive(true);
                
                var photoItem = (SO_PhotoItem) _currentMediaItemList[_currentIndex];
                var currentHeaderSprite = Sprite.Create(photoItem.texture,
                    new Rect(0, 0, photoItem.texture.width, photoItem.texture.height), Vector2.zero);

                _currentIndex += 1;
            
                mediaItemImage.sprite = currentHeaderSprite;
            }
            else if (_currentMediaItemList[_currentIndex].GetType() == typeof(SO_VideoItem))
            {
                var videoItem = (SO_VideoItem) _currentMediaItemList[_currentIndex];

                _currentIndex += 1;

                mediaItemImage.gameObject.SetActive(false);
                mediaItemVideo.clip = videoItem.videoClip;
                mediaItemVideo.Play();
            }
            
            currentItemsCountText.text = $"Item {_currentIndex}/{_currentCollection.mediaItem.MediaItemCount}";
        }
    }
    
    public void LoadPreviousMediaItem()
    {
        if (_currentIndex - 1 > 0)
        {
            _currentIndex -= 1;
            mediaItemVideo.Stop();
            
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
            
            currentItemsCountText.text = $"Item {_currentIndex}/{_currentCollection.mediaItem.MediaItemCount}";
        }
    }

    public void ToggleVolumeSlider()
    {
        volumeSlider.SetActive(!volumeSlider.activeSelf);
    }

    private void OnDisable()
    {
        StaticConfig.PublicConfig.currentCollection.mediaItem.currentMediaItemCount = _currentIndex;
        currentItemsCountText.text = "Item 13/99";
        titleText.text = "Title here";
        _currentMediaItemList.Clear();
    }
}
