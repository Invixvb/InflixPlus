using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    public class SO_MediaItem : ScriptableObject
    {
        public int mediaItemCount;
        public int currentMediaItemCount;
        
        public List<SO_PhotoItem> photoItemsList = new();
        public List<SO_VideoItem> videoItemsList = new();
    }
}
