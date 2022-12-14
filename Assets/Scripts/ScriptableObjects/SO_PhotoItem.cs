using System;
using UnityEngine;

namespace ScriptableObjects
{
    public class SO_PhotoItem : ScriptableObject
    {
        public string photoName;
        public string location;
        
        public DateTime date;
        
        public int fileSize;

        public Vector2 resolutionSize;

        public Texture2D texture;
        
        public bool seenPhotoItem;
    }
}
