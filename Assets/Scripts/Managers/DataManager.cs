using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DataManager
{
    public List<MonsterData> monsterDatas;
    public List<WeaponData> weaponDatas;
    public List<StageData> stageDatas;
    public List<BossData> bossDatas;
    public GoldData goldData;

    public void Initialize()
    {
        goldData.userName = "Default";
        Managers.FirebaseManager.ImportDataFromTable("Monster", LoadData<MonsterData>);
        Managers.FirebaseManager.ImportDataFromTable("Weapon", LoadData<WeaponData>);
        Managers.FirebaseManager.ImportDataFromTable("StageData", LoadData<StageData>);
        Managers.FirebaseManager.ImportDataFromTable("BossMonster", LoadData<BossData>);
        Managers.FirebaseManager.ImportGoldData(LoadGold);
    }

    public void LoadData<T>(List<string> datas) where T : struct
    {
        List<T> returnValue = new List<T>();
        foreach (var data in datas)
        {
            T d = JsonUtility.FromJson<T>(data);
            returnValue.Add(d);
        }
        ImportData<T>(returnValue);
    }

    public void ImportData<T>(List<T> datas) where T : struct
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
        else if (typeof(T) == typeof(BossData))
        {
            bossDatas = datas as List<BossData>;
        }
    }

    public void LoadGold(string data)
    {
        goldData = JsonUtility.FromJson<GoldData>(data);
    }

    public void ExportGold(int gold)
    {
        goldData.gold += gold;
        Managers.FirebaseManager.ExportGoldData(goldData);
    }

    public void ExportWeaponOwnership(WeaponData weaponData)
    {
        Managers.FirebaseManager.ExportWeaponOwnership(weaponData.index);
    }

}
