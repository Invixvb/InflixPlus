using System.Linq;
using Config;
using TMPro;
using UnityEngine;

public class CurrentSelectedCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;

    
    /// <summary>
    /// Get the current selected Collection.
    /// </summary>
    public void SelectCollection()
    {
        // Get the current pressed GameObject in UI
        var currentClickedButtonGo = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if (currentClickedButtonGo.name == "WatchNowButton")
        {
            foreach (var collection in StaticConfig.PublicConfig.currentUser.collectionsList
                         .Where(collection => titleText.text == collection.title))
            {
                StaticConfig.PublicConfig.currentCollection = collection;
            }
        }
        
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