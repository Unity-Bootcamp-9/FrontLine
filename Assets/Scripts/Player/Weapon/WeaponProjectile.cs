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
    [SerializeField] public Collider[] enemycollider;
    [SerializeField] private LayerMask enemy;


    public void Initialize(ObjectPool<GameObject> _pool, float _bulletSpeed, int _attackDamage, Weapon.Method _method, string _vfxPath = "")
    {
        this.pool = _pool;
        this.bulletSpeed = _bulletSpeed;
        this.attackDamage = _attackDamage;
        this.vfxPath = _vfxPath;
        this.currentMethod = _method;
        enemycollider = new Collider[5];
    }

    private void OnEnable()
    {
        shootDirection = Camera.main.transform.forward;
    }

    public void ShootProjectile()
    {
        switch (currentMethod)
        {
            case Weapon.Method.hitScan:
            case Weapon.Method.projectile:
                transform.DOMove(shootDirection * 100, 3f).OnComplete(() => { pool.Release(this.gameObject); });
                break;
            case Weapon.Method.AutoLazer:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentMethod == Weapon.Method.projectile)
        {
            if (vfxPath != "")
            {
                Managers.EffectManager.GetEffect(vfxPath, transform.position, Quaternion.identity);
            }
            if (other.CompareTag("Monster"))
            {
                Vector3 spherePivot = gameObject.transform.position;

                int enemyCount = Physics.OverlapSphereNonAlloc(spherePivot, 100, enemycollider, enemy);

                for (int i = 0; i < enemyCount; i++)
                {
                    IMonster hitMonster = enemycollider[i].GetComponentInParent<IMonster>();
                    hitMonster.GetDamage(attackDamage);
                }

                pool.Release(this.gameObject);
            }
        }
    }
}