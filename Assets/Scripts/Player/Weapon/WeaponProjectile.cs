using DamageNumbersPro;
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
    private DamageNumber dmgNumb;
    private ObjectPool<GameObject> pool;
    private int attackDamage;
    private string vfxPath;
    Weapon.Method currentMethod;
    [SerializeField] public Collider[] enemycollider;
    [SerializeField] private LayerMask enemy;
    private Tween moveTween;

    public void Initialize(ObjectPool<GameObject> _pool, float _bulletSpeed, int _attackDamage, Weapon.Method _method, DamageNumber _dmgNum, string _vfxPath = "")
    {
        this.pool = _pool;
        this.bulletSpeed = _bulletSpeed;
        this.attackDamage = _attackDamage;
        this.vfxPath = _vfxPath;
        this.currentMethod = _method;
        dmgNumb = _dmgNum;
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
                moveTween = transform.DOMove(shootDirection * 100, 2f).OnComplete(() => { pool.Release(this.gameObject); }); 
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
                    DamageNumber dmg = dmgNumb.Spawn(enemycollider[i].transform.position + new Vector3(0, 2, 0), attackDamage);
                }
                moveTween.Kill();

                pool.Release(this.gameObject);
            }
        }
    }
}