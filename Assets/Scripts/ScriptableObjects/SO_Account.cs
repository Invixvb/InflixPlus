using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
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
