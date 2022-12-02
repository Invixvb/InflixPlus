using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace ScriptableObjects
{
    public class SO_Theme : ScriptableObject
    {
        [Header("Info Text")]
        public string header;
        [TextArea] public string footer;
        
        [HideInInspector] public List<SO_Slide> levelOneSlides = new();
        [HideInInspector] public List<SO_Slide> levelTwoSlides = new();

        private void OnEnable()
        {
            if (header != null)
            {
                GetScriptableObjectsFromPath(levelOneSlides, $"Slides/{header}/Level 1/");
                GetScriptableObjectsFromPath(levelTwoSlides, $"Slides/{header}/Level 2/");
            }
        }

        private static void GetScriptableObjectsFromPath<T>(List<T> objectList, string path)
        {
            objectList.Clear();

            var type = objectList.GetType().GetGenericArguments()[0];

            var allFeedbackSOs = Resources.LoadAll(path, type);
            
            objectList.AddRange(allFeedbackSOs);
        }
    }
}
