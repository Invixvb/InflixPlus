using System.IO;
using UnityEditor;
using UnityEngine;

public class OpenFilePanel : EditorWindow
{
    [MenuItem("Tools/Import Texture")]
    private static void Apply()
    {
        var assetPath = EditorUtility.OpenFilePanel("Import an image", "", "png,jpg,jpeg");
        if (assetPath.Length != 0)
        {
            var fileContent = File.ReadAllBytes(assetPath);
            
            AssetDatabase.ImportAsset(assetPath);
            
            //AssetDatabase.CreateAsset(Texture2D.normalTexture, assetPath);
            var loadedImage = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D));
            
            loadedImage.LoadImage(fileContent);
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = loadedImage;
            
        }
    }
}