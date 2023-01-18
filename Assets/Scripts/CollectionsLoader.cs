using System.Collections.Generic;
using System.Linq;
using Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionsLoader : MonoBehaviour
{
    [Header("Top Bar")]
    [SerializeField] private TextMeshProUGUI topBarAccountText;

    [Header("Button Items")]
    [SerializeField] private List<GameObject> buttonItemList = new();

    /// <summary>
    /// Standard Unity Event.
    /// This loads in all the needed data.
    /// </summary>
    public void OnEnable()
    {
        // Get Current User
        var currentUser = StaticConfig.PublicConfig.currentUser;

        // Set the top bar account text
        topBarAccountText.text = currentUser.userName;

        // Loading all ButtonItems and set the Collection data
        for (var i = 0; i < StaticConfig.PublicConfig.currentUser.collectionsList.Count; i++)
        {
            var collection = StaticConfig.PublicConfig.currentUser.collectionsList[i];

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
                    case "ItemText":
                        var currentText = collection.title;
                        child.GetComponent<TextMeshProUGUI>().text = currentText;
                        break;
                    case "ClippingImage":
                        var currentImage = collection.mediaItem.photoItemsList[0].texture;
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
    /// Standard Unity Event.
    /// When we click the Account switching button we reset the values so we don't get old values.
    /// </summary>
    public void OnDisable()
    {
        topBarAccountText.text = "Person 1";
    }
}