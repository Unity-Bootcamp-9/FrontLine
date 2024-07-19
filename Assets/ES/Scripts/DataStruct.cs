
using System;

[System.Serializable]
public struct MonsterData
{
    public string Index;
    public float attackDamage;
    public float attackRange;
    public float hp;
    public float moveSpeed;
    public string name;
    public string projectile;
    public string sound;
}

[Serializable]
public struct WeaponData
{
    public string weaponName;
    public int  attackDamage;
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
}