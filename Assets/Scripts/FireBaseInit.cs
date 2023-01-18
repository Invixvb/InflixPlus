using Firebase;
using UnityEngine;
using UnityEngine.Events;

public class FireBaseInit : MonoBehaviour
{
    public UnityEvent onFireBaseInitialized = new();

    /// <summary>
    /// Standard Unity Event.
    /// Whenever the Database has been finished loading we call an in editor event.
    /// </summary>
    private async void Start()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            onFireBaseInitialized.Invoke();
        }
    }
}
