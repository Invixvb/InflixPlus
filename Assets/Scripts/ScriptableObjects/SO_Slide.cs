using UnityEngine;

namespace ScriptableObjects
{
    public class SO_Slide : ScriptableObject
    {
        [HideInInspector] public int level;
        [HideInInspector] public int themeListIndex;
        
        public enum SlideType { ThemeTitleScreen, ThemeOverview, AssignmentInfo, AssignmentFeedback }
        
        [Header("Slide Type")]
        public SlideType slideType;
        
        [Header("Info Text")]
        public string header;
        [TextArea(2, 10)] public string footer;
        
        [Header("Time")]
        public int assignmentTime;
        public bool timeIsChangeableByPlayerCount;

        [Header("Images")]
        public Sprite backgroundImage;
        public Sprite headerImage;

        [Header("Audio fragment")]
        public AudioClip audioFragment;
        
        [Header("UI Design")] 
        public Sprite footerBackgroundImage;
        public Sprite timerBackgroundImage;

        [Space]
        public Sprite previousButtonImage;
        public Sprite nextButtonImage;
    }
}
