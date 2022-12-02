using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    private Scene _currentScene;
    private string _sceneName;
    
    #region Singleton pattern
    private static SceneController _instance;
    public static SceneController Instance
    {
        get 
        {
            if (!_instance)
                _instance = FindObjectOfType<SceneController>();
            return _instance;
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
            ResetPrefs();
    }

    private static void ResetPrefs() => PlayerPrefs.DeleteAll();

    public void ExitGame() => Application.Quit();

    /// <summary>
    /// Here we load our scene we need async to the other scene that has already been active.
    /// When this is done we execute an AsyncOperation.
    /// </summary>
    /// <param name="sceneName"></param>
    public void AsyncLoadScene(string sceneName)
    {
        _sceneName = sceneName;

        PlayerPrefs.Save();

        _currentScene = SceneManager.GetActiveScene();

        _asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        _asyncOperation.completed += AsyncOperationOnCompleted;
    }

    /// <summary>
    /// Here we execute an AsyncOperation to set the newly loaded scene active and unload the other one
    /// </summary>
    /// <param name="obj"></param>
    private void AsyncOperationOnCompleted(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));

        SceneManager.UnloadSceneAsync(_currentScene);

        _asyncOperation.completed -= AsyncOperationOnCompleted;
    }
}
