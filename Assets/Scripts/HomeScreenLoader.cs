using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HomeScreenLoader : MonoBehaviour
{
    [Header("Top Bar")]
    [SerializeField] private TextMeshProUGUI topBarAccountText;

    private SO_Collection _featuredCollection;
    private SO_Account _currentAccount;
    
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
    [SerializeField] private List<GameObject> reWatchList = new();
    
    public void LoadCurrentAccount(SO_Account currentUser)
    {
        topBarAccountText.text = currentUser.userName;

        var randomCollection = Random.Range(0, currentUser.collectionsList.Count);
        _featuredCollection = currentUser.collectionsList[randomCollection];

        dateText.text = _featuredCollection.year.ToString();
        locationText.text = _featuredCollection.location;
        mediaItemsText.text = $"{_featuredCollection.mediaItem.MediaItemCount.ToString()} Media Items";
        titleText.text = _featuredCollection.title;
        descriptionText.text = _featuredCollection.description;

        if (_featuredCollection.mediaItem.photoItemsList.Count <= 0)
        {
            for (var i = 0; i < _featuredCollection.mediaItem.videoItemsList.Count; i++)
            {
                var videoPlayer = new VideoPlayer
                {
                    clip = _featuredCollection.mediaItem.videoItemsList[i].videoClip
                };

                var frame = videoPlayer.texture as Texture2D;
                if (frame != null)
                {
                    switch (i)
                    {
                        case 0:
                            topImage1.sprite = Sprite.Create(frame, new Rect(0, 0, frame.width, frame.height), Vector2.zero);
                            break;
                        case 1:
                            topImage2.sprite = Sprite.Create(frame, new Rect(0, 0, frame.width, frame.height), Vector2.zero);
                            break;
                        case 2:
                            topImage3.sprite = Sprite.Create(frame, new Rect(0, 0, frame.width, frame.height), Vector2.zero);
                            break;
                    }
                }
            }
        }
        else
        {
            for (var i = 0; i < _featuredCollection.mediaItem.photoItemsList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        var photoTexture1 = _featuredCollection.mediaItem.photoItemsList[1].texture;
                        topImage1.sprite = Sprite.Create(photoTexture1, new Rect(0,0, photoTexture1.width, photoTexture1.height), Vector2.zero);
                        topImage1.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(photoTexture1.width, photoTexture1.height);
                        break;
                    case 1:
                        var photoTexture2 = _featuredCollection.mediaItem.photoItemsList[2].texture;
                        topImage2.sprite = Sprite.Create(photoTexture2, new Rect(0,0, photoTexture2.width, photoTexture2.height), Vector2.zero);
                        topImage2.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(photoTexture2.width, photoTexture2.height);
                        break;
                    case 2:
                        var photoTexture3 = _featuredCollection.mediaItem.photoItemsList[3].texture;
                        topImage3.sprite = Sprite.Create(photoTexture3, new Rect(0,0, photoTexture3.width, photoTexture3.height), Vector2.zero);
                        topImage3.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(photoTexture3.width, photoTexture3.height);
                        break;
                }
            }
        }
    }

    public void WatchFeaturedCollection()
    {
        
    }

    public void AddFeaturedCollectionToWatchList()
    {
        _featuredCollection.addedToWatchList = !_featuredCollection.addedToWatchList;
    }
}
