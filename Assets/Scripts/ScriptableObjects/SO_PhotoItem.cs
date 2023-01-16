using Firebase.Storage;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "S0_PhotoItem", menuName = "ScriptableObjects/Create SO_PhotoItem", order = 4)]
    public class SO_PhotoItem : ScriptableObject
    {
        public StorageReference storageReference;
        
        [HideInInspector] public string photoName;
        
        [HideInInspector] public float fileSize;
        
        [HideInInspector] public Vector2 resolutionSize;

        public Texture2D texture;
        
        [HideInInspector] public bool seenPhotoItem;

        private void OnEnable()
        {
            if (texture != null)
            {
                // Set Name
                photoName = texture.name;
                
                // Set File size in MB
                var length = texture.EncodeToPNG().Length;
                var fileSizeInMb = length / (1024f * 1024f);

                fileSize = fileSizeInMb;

                // Set Resolution Size
                resolutionSize = new Vector2(texture.width, texture.height);
            }
        }
    }
}
