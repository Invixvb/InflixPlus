using ScriptableObjects;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_Theme))]
public class SOThemeButtonEditor : Editor
{
    /// <summary>
    /// Here we created a button to save the name changes that have been made by the user.
    /// We get the path and then change both the object and folder name for that theme.
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (SO_Theme)target;

        if (GUILayout.Button("Apply Changes"))
        {
            var themePath = AssetDatabase.GetAssetPath(script);
            var folderPath = $"Assets/Resources/Slides/{script.name}";
            
            script.name = script.header;
            AssetDatabase.RenameAsset(themePath, script.header);

            AssetDatabase.RenameAsset(folderPath, script.name);
        }
    }
}