using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;

    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                InitializeFirebase();
                
                Debug.Log("Init DB");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

    void InitializeFirebase()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GetData(string path, System.Action<DataSnapshot> callback)
    {
        databaseReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                callback(snapshot);
            }
            else if(task.IsFaulted) 
            {
                Debug.LogError("Error retrieving data from Firebase.");
            }
        });
    }

    public void SetData(string path, object data)
    {
        databaseReference.Child(path).SetValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save data: " + task.Exception);
            }
        });
    }
}
