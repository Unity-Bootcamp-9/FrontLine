using System.Collections;
using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public string weaponName { get; set; }
    public int attackDamage { get; set; }
    public float reloadTime { get; set; }
    public float fireDelay { get; set; }
    public float bulletSpeed { get; set; }
    public string bulletPrefabPaths { get; set; }
    public string gunPrefabPath { get; set; }
    public string soundFXPath { get; set; }
    public string visualFXPath { get; set; }
    public int method {  get; set; }
    public virtual void Init() { }
    public virtual void Fire() { }
    public virtual void Reload() { }
}
