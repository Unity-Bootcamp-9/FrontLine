using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DataManager
{
    public List<MonsterData> monsterDatas;
    public List<WeaponData> weaponDatas;
    public List<StageData> stageDatas;

    public void Initialize()
    {
        Managers.FirebaseManager.GetDataFromTable("Monster", LoadData<MonsterData>);
        Managers.FirebaseManager.GetDataFromTable("Weapon", LoadData<WeaponData>);
        Managers.FirebaseManager.GetDataFromTable("StageData", LoadData<StageData>);
    }

    //���̾�̽� ���� ������ ��� 
    //public void Initialize()
    //{
    //    TextAsset jsonData = Resources.Load<TextAsset>("Data/GameData");

    //    if (jsonData != null)
    //    {
    //        // JSON �����͸� GameData ��ü�� �Ľ�
    //        GameData gameData = JsonUtility.FromJson<GameData>(jsonData.text);

    //        Debug.Log(gameData.ToString());

    //        monsterDatas = gameData.Monster;
    //        weaponDatas = gameData.Weapon;
    //        stageDatas = gameData.StageData;
    //    }
    //    else
    //    {
    //        Debug.LogError("gameData.json ������ ã�� �� �����ϴ�.");
    //    }
    //}

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
        else if (typeof(T) == typeof(StageData))
        {
            stageDatas = datas as List<StageData>;
        }
    }
}
