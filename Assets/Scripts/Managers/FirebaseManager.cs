using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager
{
    private DatabaseReference databaseReference;

    public void Initialize(Action initDBManager)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase initialized");

                initDBManager?.Invoke();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

    public void GetDataFromTable(string tableName, Action<List<string>> action)
    {
        databaseReference.Child(tableName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error retrieving data from Firebase.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                List<string> data = new List<string>();

                foreach (var child in snapshot.Children)
                {
                    string item = child.GetRawJsonValue();
                    data.Add(item);
                }

                action(data);
            }
        });
    }

    public void SaveGoldData(GoldData goldData)
    {
        var goldDataMap = new Dictionary<string, object>
    {
        { "userName", goldData.userName },
        { "gold", goldData.gold }
    };
        databaseReference.Child("Gold").SetValueAsync(goldDataMap).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error saving data to Firebase: " + task.Exception);
            }
            else
            {
                Debug.Log("Data saved successfully to Firebase.");
            }
        });
    }

    public void GetGoldData(Action<string> action, string userName = "Default")
    {
        databaseReference.Child("Gold").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error retrieving data from Firebase.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                action(snapshot.GetRawJsonValue());
            }
        });
    }
}
