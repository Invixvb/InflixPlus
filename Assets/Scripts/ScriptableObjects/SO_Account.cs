using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "S0_Account", menuName = "ScriptableObjects/Create SO_Account", order = 1)]
    public class SO_Account : ScriptableObject
    {
        public string userName;
        public Texture2D userProfilePicture;
        
        public bool enabledPassword;
        public string password;
        
        public bool enabledMusic;

        public List<SO_Collection> collectionsList = new();
    }
}
