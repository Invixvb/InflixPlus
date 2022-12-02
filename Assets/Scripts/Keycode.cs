using UnityEngine;

public class Keycode : MonoBehaviour
{
    private const string CurrentPass = "93843788786573798926";

    private void Start()
    {
        if (PlayerPrefs.GetFloat("keycode") == 1)
            NextScene();
    }

    public void CheckPass(string pass)
    {
        if (pass == CurrentPass)
        {
            print("" + "Correct");
            PlayerPrefs.SetFloat("keycode", 1);
            NextScene();
        }
        else
        {
            print("Try again");
        }
    }

    private static void NextScene() => SceneController.Instance.AsyncLoadScene("MainMenu");
}
