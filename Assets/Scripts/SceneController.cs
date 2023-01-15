using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneController : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    private Scene _currentScene;
    private string _sceneName;

    [SerializeField] private VideoPlayer videoPlayer;

    private void Start() => StartCoroutine(CheckVideoFinished());
    
    /// <summary>
    /// Here we load our scene we need async to the other scene that has already been active.
    /// When this is done we execute an AsyncOperation.
    /// </summary>
    /// <param name="sceneName"></param>
    private void LoadSceneAsync(string sceneName)
    {
        // Check if the scene is already active
        if (SceneManager.GetActiveScene().name == sceneName)
            return;

        // Set the scene to use
        _sceneName = sceneName;

        // Get our current active scene
        _currentScene = SceneManager.GetActiveScene();

        // Load the scene we want to load
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // When the Operation is complete call the AsyncOperationOnCompleted method
        _asyncOperation.completed += AsyncOperationOnCompleted;
    }

    /// <summary>
    /// Here we execute an AsyncOperation to set the newly loaded scene active and unload the other one
    /// </summary>
    /// <param name="obj"></param>
    private void AsyncOperationOnCompleted(AsyncOperation obj)
    {
        // As our scene allowSceneActivation in the LoadingScreen script we can se the loaded scene to active
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));

        // We can unload the old scene
        SceneManager.UnloadSceneAsync(_currentScene);

        // Remove it from the memory
        _asyncOperation.completed -= AsyncOperationOnCompleted;
    }

    /// <summary>
    /// Check if Video finished playing.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckVideoFinished()
    {
        videoPlayer.Play();

        yield return new WaitForSeconds(8.5f);
        
        LoadSceneAsync("MainScene");
    }
}