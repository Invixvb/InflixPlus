using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "S0_MediaItem", menuName = "ScriptableObjects/Create SO_MediaItem", order = 3)]
    public class SO_MediaItem : ScriptableObject
    {
        public int MediaItemCount => photoItemsList.Count + videoItemsList.Count;
        [HideInInspector] public int currentMediaItemCount;
        
        public List<SO_PhotoItem> photoItemsList = new();
        public List<SO_VideoItem> videoItemsList = new();
    }
}
