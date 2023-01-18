using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhosWatchingLoader : MonoBehaviour
{
    public TextMeshProUGUI personText1;
    public TextMeshProUGUI personText2;

    public Image personIcon1;
    public Image personIcon2;

    public List<SO_Account> soAccounts = new();

    /// <summary>
    /// Standard Unity Event.
    /// Loads all the data into the WhosWatching screen UI.
    /// </summary>
    private void Awake()
    {
        var user1 = soAccounts[0];
        var user2 = soAccounts[1];

        personText1.text = user1.userName;
        personText2.text = user2.userName;

        personIcon1.sprite = user1.userProfilePicture;
        personIcon2.sprite = user2.userProfilePicture;
    }
}