
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
}

[Serializable]
public struct StageData
{
    public int stage;
    public int[] monsterIndexs;
    public float[] spawnDelays;
}

public class GameData
{
    public List<MonsterData> Monster;
    public List<WeaponData> Weapon;
    public List<StageData> StageData;
}
