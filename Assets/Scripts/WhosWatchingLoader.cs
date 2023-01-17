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

    private void Awake()
    {
        var user1 = soAccounts[0];
        var user2 = soAccounts[1];
        
        if (user1.userName != null) 
            personText1.text = user1.userName;
        if (user2.userName != null) 
            personText2.text = user2.userName;

        if (user1.userProfilePicture != null) 
            personIcon1.sprite = user1.userProfilePicture;
        if (user2.userProfilePicture != null) 
            personIcon2.sprite = user2.userProfilePicture;
    }
}