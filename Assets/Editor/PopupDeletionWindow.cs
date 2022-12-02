using System.Collections.Generic;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

public class PopupDeletionWindow : EditorWindow
{
    private int _themeIndex;
    private int _levelIndex;

    private static List<SO_Theme> _themeList;

    /// <summary>
    /// Here we initiate the popup window for .
    /// It gets the themeList from the other script to use it in here.
    /// We create and instance of the script, set all the parameters for it to show nicely, and then show the Modal we created.
    /// </summary>
    /// <param name="themeList"></param>
    public static void Init(List<SO_Theme> themeList)
    {
        _themeList = themeList;

        EditorWindow window = CreateInstance<PopupDeletionWindow>();

        var titleContent = new GUIContent("Delete Object");
        window.titleContent = titleContent;

        window.position = new Rect(670, 380, 200, 80);

        window.minSize = new Vector2(200, 80);
        window.maxSize = new Vector2(200, 80);

        window.ShowModalUtility();
    }

    /// <summary>
    /// Standard Unity Event.
    /// We use this to ask the user if they really want to delete the specified object.
    /// Here we check if the user selected a type SO_Slide or type SO_Theme in the asset folder.
    /// When clicked "yes", the script will act accordingly to the type of object that has been selected.
    /// We delete all the folders, the asset itself, and remove it from the list in the case of an SO_Theme.
    /// And we delete the asset and remove it from the specified list for the SO_Slide type.
    /// If either a wrong type has been selected or some error occurs. We throw an error in the console.
    /// When clicked on "no" we just close the popup.
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label($"Do you want to delete {Selection.activeObject.name}?");

        GUILayout.Space(35);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Yes"))
        {
            switch (Selection.activeObject)
            {
                case SO_Theme themeItem:
                {
                    AssetDatabase.DeleteAsset($"Assets/Resources/Slides/{themeItem.name}/Level 1");
                    AssetDatabase.DeleteAsset($"Assets/Resources/Slides/{themeItem.name}/Level 2");
                    AssetDatabase.DeleteAsset($"Assets/Resources/Slides/{themeItem.name}");

                    var themePath = AssetDatabase.GetAssetPath(themeItem);
                    AssetDatabase.DeleteAsset(themePath);

                    _themeList.Remove(themeItem);
                    break;
                }
                case SO_Slide item:
                {
                    var path = AssetDatabase.GetAssetPath(item);
                    AssetDatabase.DeleteAsset(path);

                    switch (item.level)
                    {
                        case 1:
                            _themeList[item.themeListIndex].levelOneSlides.Remove(item);
                            break;
                        case 2:
                            _themeList[item.themeListIndex].levelTwoSlides.Remove(item);
                            break;
                        default:
                            Debug.LogError("Can't reach level index");
                            break;
                    }

                    break;
                }
                default:
                    Debug.LogError("Error while trying to delete the object.");
                    break;
            }

            Close();
        }

        if (GUILayout.Button("No"))
            Close();

        GUILayout.EndHorizontal();
    }
}
