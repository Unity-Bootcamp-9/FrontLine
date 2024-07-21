using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponProjectile : MonoBehaviour
{
    private float bulletSpeed;
    private Vector3 shootDirection;
    private ObjectPool<GameObject> pool;
    private const float lifeTime = 5f;
    private float elapsedTime;

    public void Initialize(ObjectPool<GameObject> _pool, float _bulletSpeed)
    {
        this.pool = _pool;
        this.bulletSpeed = _bulletSpeed;
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
}
