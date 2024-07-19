using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagerTest : MonoBehaviour
{
    public FirebaseWriteExample firebaseWriteExample;

    public List<MonsterData> monsterDatas;
    public List<WeaponData> weaponData;

    private void Start()
    {
        StartCoroutine(TestCo());
    }

    IEnumerator TestCo()
    {
        yield return new WaitForSeconds(1);

        firebaseWriteExample.GetDataFromTable("Monster", LoadData<MonsterData>);
        firebaseWriteExample.GetDataFromTable("Weapon", LoadData<WeaponData>);
    }

    public void Init()
    {
        firebaseWriteExample.GetDataFromTable("Monster", LoadData<MonsterData>);
        firebaseWriteExample.GetDataFromTable("Weapon", LoadData<WeaponData>);
    }

    public void LoadData<T>(List<string> datas) where T : struct
    {
        List<T> returnValue = new List<T>();
        foreach (var data in datas)
        {
            T d = JsonUtility.FromJson<T>(data);
            returnValue.Add(d);
        }
        SaveData<T>(returnValue);
    }

    public void SaveData<T>(List<T> datas) where T : struct
    {
        if (typeof(T) == typeof(MonsterData))
        {
            monsterDatas = datas as List<MonsterData>;
        }
        else if (typeof(T) == typeof(WeaponData))
        {
            weaponData = datas as List<WeaponData>;
        }
    }
}
