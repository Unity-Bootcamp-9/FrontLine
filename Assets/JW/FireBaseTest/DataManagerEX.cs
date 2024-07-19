using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagerEX : MonoBehaviour
{
    public FirebaseManager firebaseManager;

    private List<object> dataList = new List<object>();

    public void ButtonClick()
    {
        firebaseManager.GetData("Monster", OnDataReceived<MonsterData>);

        StartCoroutine(PrintData<MonsterData>(2.0f));

        foreach (var data in GetAllData<MonsterData>())
        {
            Debug.Log(data);
        }
    }

    private IEnumerator PrintData<T>(float delay)
    {
        yield return new WaitForSeconds(delay);
        List<T> allData = GetAllData<T>();

        foreach (T data in allData)
        {
            Debug.Log(JsonUtility.ToJson(data, true)); // JSON 형식으로 데이터 출력
        }
    }

    void OnDataReceived<T>(Firebase.Database.DataSnapshot snapshot)
    {
        foreach (Firebase.Database.DataSnapshot childSnapshot in snapshot.Children)
        {
            T data = JsonUtility.FromJson<T>(childSnapshot.GetRawJsonValue());
            dataList.Add(data);
        }
    }

    public List<T> GetAllData<T>()
    {
        List<T> typedList = new List<T>();

        foreach (object data in dataList)
        {
            if (data is T)
            {
                typedList.Add((T)data);
            }
        }

        return typedList;
    }

    public void SaveData<T>(string key, T data)
    {
        dataList.Add(data);
        string json = JsonUtility.ToJson(data);
        firebaseManager.SetData("path/to/data/" + key, json);
    }
}