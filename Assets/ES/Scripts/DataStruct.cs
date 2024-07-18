
using System;

[System.Serializable]
public struct MonsterData
{
    public string Index { get; set; }
    public float attackDamage { get; set; }
    public float attackRange { get; set; }
    public float hp { get; set; }
    public float moveSpeed { get; set; }
    public string name { get; set; }
    public string projectile { get; set; }
    public string sound { get; set; }

    public MonsterData(string _index, float _attackDamage, float _attackRange,float _hp, float _moveSpeed, string _name, string _projectile, string _sound)
    {
        Index = _index;
        attackDamage = _attackDamage;
        attackRange = _attackRange;
        hp = _hp;
        moveSpeed = _moveSpeed;
        name = _name;
        projectile = _projectile;
        sound = _sound;
    }
}

[Serializable]
public struct WeaponData
{
    public string weaponName { get; set; }
    public float attackDamage { get; set; }
    public float reloadTime { get; set; }
    public float fireDelay { get; set; }
    public int bulletCount { get; set; }
    public string bulletPrefab { get; set; }
    public string soundFX { get; set; }
    public string visualFX { get; set; }
    public float bulletSpeed { get; set; }
    public float range { get; set; }
    public string bulletVisualFX { get; set; }
    public int method { get; set; }

    public WeaponData(string _weaponName, float _attackDamage, float _reloadTime, float _fireDelay, int _bulletCount, string _bulletPrefab,
        string _soundFX, string _visualFX, float _bulletSpeed, float _range, string _bulletVisualFX, int _Method)
    {
        weaponName = _weaponName;
        attackDamage = _attackDamage;
        reloadTime = _reloadTime;
        fireDelay = _fireDelay;
        bulletCount = _bulletCount;
        bulletPrefab = _bulletPrefab;
        soundFX = _soundFX;
        visualFX = _visualFX;
        bulletSpeed = _bulletSpeed;
        range = _range;
        bulletVisualFX = _bulletVisualFX;
        method = _Method;
    }
}