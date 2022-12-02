using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    private Scene _currentScene;
    private string _sceneName;

    /// <summary>
    /// Load MainScene when Intro is done playing
    /// </summary>
    private void LoadMainScene() => AsyncLoadScene("MainScene");

    /// <summary>
    /// Here we load our scene we need async to the other scene that has already been active.
    /// When this is done we execute an AsyncOperation.
    /// </summary>
    /// <param name="sceneName"></param>
    private void AsyncLoadScene(string sceneName)
    {
        _sceneName = sceneName;

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