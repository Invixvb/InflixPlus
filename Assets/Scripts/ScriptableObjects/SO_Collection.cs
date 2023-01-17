using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "S0_Collection", menuName = "ScriptableObjects/Create SO_Collection", order = 2)]
    public class SO_Collection : ScriptableObject
    {
        public string title;
        [TextArea(2,10)] public string description;
        public string location;

        public int year;

        [HideInInspector] public bool addedToWatchList;
        [HideInInspector] public bool startedWatching; //TODO: Set this bool somewhere
        [HideInInspector] public bool seenCollection; //TODO: Set this bool somewhere

        public SO_MediaItem mediaItem;
    }
}
