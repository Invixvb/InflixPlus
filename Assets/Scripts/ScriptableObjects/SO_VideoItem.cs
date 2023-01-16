using Firebase.Storage;
using UnityEngine;
using UnityEngine.Video;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "S0_VideoItem", menuName = "ScriptableObjects/Create SO_VideoItem", order = 5)]
    public class SO_VideoItem : ScriptableObject
    {
        public StorageReference storageReference;
        
        [HideInInspector] public string videoName;
        
        [HideInInspector] public double lenght;
        [HideInInspector] public int savedLenght;

        [HideInInspector] public double fileSize;

        [HideInInspector] public Vector2 resolutionSize;

        public VideoClip videoClip;

        [HideInInspector] public bool seenVideoItem;
        
        private void OnEnable()
        {
            if (videoClip != null)
            {
                // Set Name
                videoName = videoClip.name;
                
                // Set the Lenght
                lenght = videoClip.length;
                
                // Set the File size
                var clipBitrate = videoClip.width * videoClip.height * videoClip.frameRate;
                fileSize = lenght * clipBitrate / 8 / 1048576;

                // Set Resolution Size
                resolutionSize = new Vector2(videoClip.width, videoClip.height);
            }
        }
    }
}
