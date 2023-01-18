using System.Linq;
using Config;
using TMPro;
using UnityEngine;

public class CurrentSelectedCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    
    /// <summary>
    /// Get the current selected Collection whenever we click a button that opens a CollectionScreen.
    /// </summary>
    public void SelectCollection()
    {
        // Get the current pressed GameObject in UI
        var currentClickedButtonGo = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        // If it is the WatchNow button we need to gather it's collection differently
        if (currentClickedButtonGo.name == "WatchNowButton")
        {
            // Get all the collections in the currentUser collectionList,
            // and we check if the featuredCollection title has the same name as the indexed collection title
            foreach (var collection in StaticConfig.PublicConfig.currentUser.collectionsList
                         .Where(collection => titleText.text == collection.title))
            {
                StaticConfig.PublicConfig.currentCollection = collection;
            }
        }
        
        // Get all the child GOs in a CollectionItemButton, then we add it to a list.
        // Then we get every collection in the currentUser collection list,
        // and we check the child GOs in the list and check if their name is either "TitleText" or "ItemText".
        // Then if the titleText is equal to the collectionTitle we add the collection as the context of the currentCollection
        foreach (var collection in StaticConfig.PublicConfig.currentUser.collectionsList
                     .Select(collection => new { collection, childrenObjectList = currentClickedButtonGo
                             .GetComponentsInChildren<Transform>(true)
                             .Select(transform1 => transform1.gameObject)
                             .ToList()})
                     .SelectMany(list => list.childrenObjectList, (list, child) => new {t = list, child})
                     .Where(collection => collection.child.name is "TitleText" or "ItemText")
                     .Where(collection => collection.child.GetComponent<TextMeshProUGUI>().text == collection.t.collection.title)
                     .Select(collection => collection.t.collection))
        {
            StaticConfig.PublicConfig.currentCollection = collection;
        }
    }
}