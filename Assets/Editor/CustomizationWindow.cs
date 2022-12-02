using System.Collections.Generic;
using System.Linq;
using Configs;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CustomizationWindow : EditorWindow
{ 
    private int _toolbarIntTheme;
    private int _toolbarIntLevel;

    private readonly string[] _levelOfDifficultyArray = { "Level 1", "Level 2" };

    private string[] _themeNamesArray;
    private readonly List<string> _themeHeadersList = new();

    public List<SO_Theme> themeList = new();
    public List<ScriptableObject> assignmentList = new();

    [MenuItem("Window/Game Customization Window")]
    public static void ShowWindow()
    {
        GetWindow<CustomizationWindow>("Game Customization");
    }

    private void Awake()
    {
        GetScriptableObjectsFromPath(themeList, "Themes/");
    }

    private void OnFocus()
    {
        UpdateThemeHeaders();
    }

    private void OnValidate()
    {
        RememberSelectedButton();
        
        StaticConfig.PublicConfig.themeList.Clear();
        StaticConfig.PublicConfig.themeList.AddRange(themeList);
    }

    /// <summary>
    /// Here we get all the Headers from the theme scriptable objects and add them to a list.
    /// Then we add the list content to an array. So it can be integrated into the menu buttons.
    /// </summary>
    private void UpdateThemeHeaders()
    {
        _themeHeadersList.Clear();

        foreach (var themeHeader in themeList.Select(theme => theme.header))
        {
            _themeHeadersList.Add(themeHeader);
        }

        _themeNamesArray = _themeHeadersList.ToArray();
    }

    /// <summary>
    /// Here we get the current index of the selected button.
    /// Then we update the theme headers so we make sure we have the latest headers.
    /// Then we check which one we have selected and set the toolbar int to it.
    /// </summary>
    private void RememberSelectedButton()
    {
        if (_themeNamesArray == null || _toolbarIntTheme > 0 && _toolbarIntTheme < _themeNamesArray.Length) return;
        
        var currentSelection = _themeNamesArray[_toolbarIntTheme];

        UpdateThemeHeaders();

        for (var i = 0; i < _themeNamesArray.Length; i++)
        {
            if (currentSelection == _themeNamesArray[i])
            {
                _toolbarIntTheme = i;
            }
        }
    }

    /// <summary>
    /// Here we update the assignmentList on which button in the Toolbars are selected.
    /// So when theme 1 and level 2 are selected the level 2 list of that theme will be loaded into assignmentList.
    /// </summary>
    private void GetEditButtonsSelected()
    {
        assignmentList.Clear();
        
        switch (_toolbarIntLevel)
        {
            case 0:
                assignmentList.AddRange(themeList[_toolbarIntTheme].levelOneSlides);
                break;
            case 1:
                assignmentList.AddRange(themeList[_toolbarIntTheme].levelTwoSlides);
                break;
            default:
                Debug.LogError("Can't reach level index");
                break;
        }
    }

    /// <summary>
    /// Create a theme by pressing the button, here we create a unique asset from SO_Theme and set it to the path.
    /// Then we focus it in our Project window and add it to the list.
    /// We also add the necessary folders for that theme and update the headers.
    /// </summary>
    private void CreateTheme()
    {
        var themeObject = CreateInstance<SO_Theme>();

        var path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Themes/Theme.asset");
        AssetDatabase.CreateAsset(themeObject, path);
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = themeObject;

        themeObject.header = themeObject.name;

        const string themeFolderPath = "Assets/Resources/Slides";
        AssetDatabase.CreateFolder(themeFolderPath, themeObject.name);
            
        var levelFolderPath = $"Assets/Resources/Slides/{themeObject.name}";
        AssetDatabase.CreateFolder(levelFolderPath, "Level 1");
        AssetDatabase.CreateFolder(levelFolderPath, "Level 2");

        themeList.Add(themeObject);
            
        UpdateThemeHeaders();
    }
    
    /// <summary>
    /// Get all the scriptable objects in a certain path, any kind of list can be given here. As well as any path.
    /// It will get all the objects in that path and place it all in the list.
    /// </summary>
    /// <param name="objectList"></param>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    private static void GetScriptableObjectsFromPath<T>(List<T> objectList, string path)
    {
        objectList.Clear();

        var type = objectList.GetType().GetGenericArguments()[0];

        var allFeedbackSOs = Resources.LoadAll(path, type);
        objectList.AddRange(allFeedbackSOs);
    }

    /// <summary>
    /// Standard Unity Event.
    /// We use this to display the editor window on screen and load all the UI components into it.
    /// </summary>
    private void OnGUI()
    {
        var so = new SerializedObject(this);
        
        GUILayout.Space(5);

        GUILayout.Label("Create Objects", EditorStyles.boldLabel);
        
        GUILayout.Space(4);

        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Create Theme"))
        {
            CreateTheme();
        }

        if (GUILayout.Button("Create Assignment"))
        {
            PopupAssignmentCreateWindow.Init(_themeNamesArray, themeList);
            
            GetEditButtonsSelected();
        }

        GUILayout.EndHorizontal();
        
        if (GUILayout.Button("Delete Object"))
        {
            if (Selection.activeObject is SO_Theme or SO_Slide )
            {
                PopupDeletionWindow.Init(themeList);
            
                UpdateThemeHeaders();
                GetEditButtonsSelected();
            }
            else
            {
                Debug.LogError("The selected object is not of type SO_Theme or SO_Slide");
            }
        }

        GUILayout.Space(15);

        GUILayout.Label("Edit Themes", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(so.FindProperty("themeList"));
        
        GUILayout.Label("Edit Assignments", EditorStyles.boldLabel);
        
        GUILayout.Space(5);
        
        EditorGUI.BeginChangeCheck();

        _toolbarIntTheme = GUILayout.Toolbar(_toolbarIntTheme, _themeNamesArray);
        _toolbarIntLevel = GUILayout.Toolbar(_toolbarIntLevel, _levelOfDifficultyArray);

        if (EditorGUI.EndChangeCheck())
        {
            GetEditButtonsSelected();
        }

        GUILayout.Space(5);

        EditorGUILayout.PropertyField(so.FindProperty("assignmentList"));

        so.ApplyModifiedProperties();
    }
}
