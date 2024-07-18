using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagerTest : MonoBehaviour
{
    public FirebaseWriteExample firebaseWriteExample;

    private void Start()
    {
        StartCoroutine(TestCo());
    }

    IEnumerator TestCo()
    {
        yield return new WaitForSeconds(1);
        firebaseWriteExample.GetDataFromTable("Monster", LoadData<MonsterData>);
    }
    public void LoadData<T>(List<string> datas) where T : struct
    {
        foreach (var data in datas)
        {
            T d = JsonUtility.FromJson<T>(data);
            Debug.Log(d);
        }
    }
}
