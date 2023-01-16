using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhosWatchingLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI personText1;
    [SerializeField] private TextMeshProUGUI personText2;

    [SerializeField] private Image personIcon1;
    [SerializeField] private Image personIcon2;

    [SerializeField] private List<SO_Account> soAccountList = new();

    private void Awake()
    {
        var user1 = soAccountList[0];
        var user2 = soAccountList[1];
        
        if (user1.userName != null) personText1.text = user1.userName;
        if (user2.userName != null) personText2.text = user2.userName;

        if (user1.userProfilePicture != null) personIcon1.sprite = user1.userProfilePicture;
        if (user2.userProfilePicture != null) personIcon2.sprite = user2.userProfilePicture;
    }
}