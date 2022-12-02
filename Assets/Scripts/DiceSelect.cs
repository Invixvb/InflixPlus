using Configs;
using UnityEngine;

public class DiceSelect : MonoBehaviour
{
    public void SelectTheme(int themeIndex)
    {
        if (themeIndex == 5)
        {
            themeIndex = Random.Range(0, 5);
        }
        
        StaticConfig.PublicConfig.themeSelectedIndex = themeIndex;
        
        SceneController.Instance.AsyncLoadScene("Level");
    }
}
