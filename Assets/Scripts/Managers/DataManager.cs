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

    //파이어베이스 연결 전까지 사용 
    //public void Initialize()
    //{
    //    TextAsset jsonData = Resources.Load<TextAsset>("Data/GameData");

    //    if (jsonData != null)
    //    {
    //        // JSON 데이터를 GameData 객체로 파싱
    //        GameData gameData = JsonUtility.FromJson<GameData>(jsonData.text);

    //        Debug.Log(gameData.ToString());

    //        monsterDatas = gameData.Monster;
    //        weaponDatas = gameData.Weapon;
    //        stageDatas = gameData.StageData;
    //    }
    //    else
    //    {
    //        Debug.LogError("gameData.json 파일을 찾을 수 없습니다.");
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
