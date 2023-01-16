using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "S0_Collection", menuName = "ScriptableObjects/Create SO_Collection", order = 2)]
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
