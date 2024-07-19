using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public List<MonsterData> monsterDatas;
    public List<WeaponData> weaponDatas;
    public void Initialize()
    {
        Managers.FirebaseManager.GetDataFromTable("Monster", LoadData<MonsterData>);
        Managers.FirebaseManager.GetDataFromTable("Weapon", LoadData<WeaponData>);
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
            weaponDatas = datas as List<WeaponData>;
        }
    }
}
