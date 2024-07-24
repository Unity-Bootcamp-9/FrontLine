using Firebase;
using Firebase.Extensions;
using System;
using UnityEngine;

public class FirebaseInitializer
{
    private FirebaseApp app;

    public void Init(Action initManagers)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;

                Debug.Log("Firebase is ready to use.");

                initManagers?.Invoke();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }

        });
    }

}
