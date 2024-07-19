
using System;

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

[Serializable]
public struct WeaponData
{
    public string weaponName;
    public int attackDamage;
    public float reloadTime;
    public float fireDelay;
    public int bulletCount;
    public string bulletPrefab;
    public string soundFX;
    public string visualFX;
    public float bulletSpeed;
    public float range;
    public string bulletVisualFX;
    public int method;
}