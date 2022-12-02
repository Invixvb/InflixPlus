using System.Collections.Generic;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

public class PopupAssignmentCreateWindow : EditorWindow
{
    private int _themeIndex;
    private int _levelIndex;

    private static string[] _themeArray;
    private static List<SO_Theme> _themeList;
    private static readonly string[] LevelArray = { "Level 1", "Level 2" };

    /// <summary>
    /// Here we initiate the popup window for creating an assignment.
    /// It gets the themeArray and themeList from the other script to use it in here.
    /// We create and instance of the script, set all the parameters for it to show nicely, and then show the Modal we created.
    /// </summary>
    /// <param name="themeArray"></param>
    /// <param name="themeList"></param>
    public static void Init(string[] themeArray, List<SO_Theme> themeList)
    {
        _themeArray = themeArray;
        _themeList = themeList;

        EditorWindow window = CreateInstance<PopupAssignmentCreateWindow>();

        var titleContent = new GUIContent("Create Assignment");
        window.titleContent = titleContent;

        window.position = new Rect(670, 380, 200, 100);

        window.minSize = new Vector2(200, 100);
        window.maxSize = new Vector2(200, 100);

        window.ShowModalUtility();
    }

    /// <summary>
    /// Standard Unity Event.
    /// This gets called whenever the Modal is shown. Then we display the 2 arrays with all the information in them.
    /// When we've selected our options, we create a new slide and generate the asset and focus on it in the asset folder.
    /// We also add it to the list depending on what level they selected.
    /// </summary>
    private void OnGUI()
    {
        _themeIndex = EditorGUILayout.Popup(_themeIndex, _themeArray);
        _levelIndex = EditorGUILayout.Popup(_levelIndex, LevelArray);

        GUILayout.Space(35);
        
        if (GUILayout.Button("Create"))
        {
            var themeObject = CreateInstance<SO_Slide>();

            var path = AssetDatabase.GenerateUniqueAssetPath($"Assets/Resources/Slides/{_themeList[_themeIndex].name}/{LevelArray[_levelIndex]}/Slide.asset");
            AssetDatabase.CreateAsset(themeObject, path);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = themeObject;

            switch (_levelIndex)
            {
                case 0:
                    _themeList[_themeIndex].levelOneSlides.Add(themeObject);
                    themeObject.level = 1;
                    break;
                case 1:
                    _themeList[_themeIndex].levelTwoSlides.Add(themeObject);
                    themeObject.level = 2;
                    break;
                default:
                    Debug.LogError("Level index not found");
                    break;
            }

            themeObject.themeListIndex = _themeIndex;

            Close();
        }
    }
}
