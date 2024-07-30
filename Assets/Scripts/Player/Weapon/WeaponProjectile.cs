using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class WeaponProjectile : MonoBehaviour
{
    private float bulletSpeed;
    private Vector3 shootDirection;
    private ObjectPool<GameObject> pool;
    private int attackDamage;
    private string vfxPath;
    Weapon.Method currentMethod;
    AudioSource weaponProjectileAudio;

    public void Initialize(ObjectPool<GameObject> _pool, float _bulletSpeed, int _attackDamage, Weapon.Method _method, string _vfxPath = "")
    {
        this.pool = _pool;
        this.bulletSpeed = _bulletSpeed;
        this.attackDamage = _attackDamage;
        this.vfxPath = _vfxPath;
        this.currentMethod = _method;
    }

    private void OnEnable()
    {
        shootDirection = Camera.main.transform.forward;
    }

    public void ShootProjectile()
    {
        if(weaponProjectileAudio == null)
        {
            weaponProjectileAudio = gameObject.AddComponent<AudioSource>();
        }

        switch (currentMethod)
        {
            case Weapon.Method.hitScan:
                transform.DOMove(shootDirection * 100, 3f).OnComplete(() => { pool.Release(this.gameObject); });
                weaponProjectileAudio.PlayOneShot(Managers.SoundManager.GetAudioClip("pistol"));
                break;
            case Weapon.Method.projectile:
                transform.DOMove(shootDirection * 100, 3f).OnComplete(() => { pool.Release(this.gameObject); });
                weaponProjectileAudio.PlayOneShot(Managers.SoundManager.GetAudioClip("misslelauncher"));
                break;
            case Weapon.Method.AutoLazer:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (currentMethod == Weapon.Method.projectile)
        {
            Debug.Log("method");

            if (other.CompareTag("Monster"))
            {
                if(vfxPath != "") 
                {
                    Managers.EffectManager.GetEffect(vfxPath, transform.position, Quaternion.identity);
                }

                Monster hitTarget = other.GetComponentInParent<Monster>();
                hitTarget.GetDamage(attackDamage);
                Destroy(gameObject);
                Debug.Log("monster");
            }

            if(other.CompareTag("MonsterProjectile"))
            {
                Destroy(other.gameObject);
                pool.Release(this.gameObject);
            }
        }
    }
}
