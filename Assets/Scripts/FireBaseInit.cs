using Firebase;
using UnityEngine;
using UnityEngine.Events;

public class FireBaseInit : MonoBehaviour
{
    public UnityEvent onFireBaseInitialized = new();

    private async void Start()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
            onFireBaseInitialized.Invoke();
        }
    }
}
