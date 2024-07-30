
using System;
using System.Collections.Generic;

[System.Serializable]
public struct MonsterData
{
    public int index;
    public int attackDamage;
    public float attackRange;
    public int hp;
    public float moveSpeed;
    public string name;
    public string projectile;
    public string sound;
    public int gold;
}

[System.Serializable]
public struct BossData
{
    public int index;
    public int attackDamage;
    public float attackRange;
    public int hp;
    public float moveSpeed;
    public string name;
    public string projectile;
    public string sound;
}

[Serializable]
public struct WeaponData
{
    public int index;
    public string weaponName;
    public int attackDamage;
    public float reloadTime;
    public float fireDelay;
    public int bulletCount;
    public string weaponPrefab;
    public string bulletPrefab;
    public string soundFX;
    public string visualFX;
    public float bulletSpeed;
    public float range;
    public string bulletVisualFX;
    public int method;
    public string info;
    public int price;
    public bool isOwn;

    public Dictionary<string, object> ReturnDic()
    {
        Dictionary<string, object> keyValuePairs = new Dictionary<string, object>
        {
            { "index", index },
            { "weaponName", weaponName},
            { "attackDamage", attackDamage },
            { "reloadTime", reloadTime },
            { "fireDelay", fireDelay },
            { "bulletCount", bulletCount },
            { "weaponPrefab", weaponPrefab },
            { "bulletPrefab", bulletPrefab },
            { "soundFX", soundFX },
            { "visualFX", visualFX },
            { "bulletSpeed", bulletSpeed },
            { "range", range },
            { "bulletVisualFX", bulletVisualFX },
            { "method", method },
            { "info", info },
            { "price", price },
            { "isOwn", isOwn }
        };
        return keyValuePairs;
    }
}

[Serializable]
public struct StageData
{
    public int stage;
    public int bossIndex;
    public int[] monsterIndexs;
    public float[] spawnDelays;
}

[Serializable]
public struct GoldData
{
    public string userName;
    public int gold;
}

public class GameData
{
    public List<MonsterData> Monster;
    public List<WeaponData> Weapon;
    public List<StageData> StageData;
}
