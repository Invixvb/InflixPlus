using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerWarning : MonoBehaviour
{
    private bool _gameStarted, _gameIsOver, _firstMark, _checkTwice;
    private float _timeAmount;

    [SerializeField] private TextMeshProUGUI text;
    public GameObject popUpPanel, endPopUpPanel;
    public float timeInMinutes;
    public float timeLeft;
    public bool noTimeLeft;

    private void Start()
    {
        _timeAmount = timeInMinutes * 60;
        timeLeft = _timeAmount;
        _gameStarted = true;

        if (timeInMinutes > 5)
        {
            _checkTwice = true;
        } 
    }

    private void Update()
    {
        if (_gameStarted && !_gameIsOver && timeLeft > 0)
        {
            timeLeft -= 1 * Time.deltaTime;
        }

        if (timeLeft <= 20 * 60 && !_firstMark)
        {
            if (_checkTwice)
            {
                PopUp("20 MINUTES LEFT");
            }

            _firstMark = true;
        }

        if (timeLeft <= 5 * 60 && _firstMark && !noTimeLeft)
        {
            PopUp("5 MINUTES LEFT");
            noTimeLeft = true;
        }

        if (timeLeft <= 0.1f && noTimeLeft)
        {
            EndPopUp();
        }
    }

    private void PopUp(string timeAmount)
    {
        text.SetText(timeAmount);
        popUpPanel.SetActive(true);
    }

    private void EndPopUp()
    {
        endPopUpPanel.SetActive(true);
    }

    public void ResumeWarning()
    {
        popUpPanel.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
