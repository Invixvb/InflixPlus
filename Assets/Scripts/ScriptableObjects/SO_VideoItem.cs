using System;
using UnityEngine;
using UnityEngine.Video;

namespace ScriptableObjects
{
    public class SO_VideoItem : ScriptableObject
    {
        public string videoName;
        
        public DateTime date;
        public DateTime lenght;
        public DateTime storedLenght;
        
        public int fileSize;

        public Vector2 resolutionSize;

        public VideoClip videoClip;

        public bool seenVideoItem;
    }
}
