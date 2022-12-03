using System;
using UnityEngine;

namespace ScriptableObjects
{
    public class SO_Collection : ScriptableObject
    {
        public string title;
        public string description;
        public string place;
        
        public DateTime date;

        public bool addedToWatchList;
        public bool seenCollection;

        public SO_MediaItem mediaItem = new();
    }
}
