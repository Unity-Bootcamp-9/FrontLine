using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirebaseWriteExample : MonoBehaviour
{
    private DatabaseReference databaseReference;
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                InitializeFirebase();
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
        Debug.Log("Firebase initialized");

        GetDataFromTable("Monster");
    }

    private void WriteNewUser(string userId, string name, string email)
    {
        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);

        databaseReference.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                Debug.Log("Data written successfully");
            }
            else
            {
                Debug.LogError("Failed to write data: " + task.Exception);
            }
        });
    }

    public void GetDataFromTable(string tableName)
    {
        databaseReference.Child(tableName).GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Error retrieving data from Firebase.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.GetRawJsonValue());

                snapshot.GetRawJsonValue();
                Debug.Log(JsonUtility.ToJson(snapshot));
                // Parse and use the data from snapshot
                //foreach (var child in snapshot.Children)
                //{
                //    string itemKey = child.Key;
                //
                //    MonsterData item = JsonUtility.FromJson<MonsterData>(child.GetRawJsonValue());
                //    
                //    Debug.Log(item.name + item.Index + item.projectile);
                //    
                //}
            }
        });
    }

}

[System.Serializable]
public class User
{
    public string username;
    public string email;

    public User(string username, string email)
    {
        this.username = username;
        this.email = email;
    }
}
