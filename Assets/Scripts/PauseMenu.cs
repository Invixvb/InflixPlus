using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused;

    public GameObject objPauseMenu;

    private void Start()
    {
        objPauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        objPauseMenu.SetActive(true);
        _isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        objPauseMenu.SetActive(false);
        _isPaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
