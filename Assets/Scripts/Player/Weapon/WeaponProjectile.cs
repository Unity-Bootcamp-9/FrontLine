using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponProjectile : MonoBehaviour
{
    private float bulletSpeed;
    private Vector3 shootDirection;
    private ObjectPool<GameObject> pool;
    private int attackDamage;
    private const float lifeTime = 5f;
    private float elapsedTime;
    private string vfxPath;
    Weapon.Method currentMethod;

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
        elapsedTime = 0f;
    }

    void Update()
    {
        transform.position += shootDirection * Time.deltaTime * bulletSpeed;

        elapsedTime += Time.deltaTime;

        if (elapsedTime > lifeTime)
        {
            pool.Release(this.gameObject);
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
            }

            if(other.CompareTag("MonsterProjectile"))
            {
                Destroy(other.gameObject);
                pool.Release(this.gameObject);
            }
        }
    }
}
