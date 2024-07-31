using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Monster : MonoBehaviour, IMonster
{
    public Vector3 playerPos;
    private Animator animator;
    public MonsterData monsterData;
    public Portal portal;
    protected int currentHP;
    public Rigidbody rigidBody;
    [SerializeField] private Transform firePos;

    private static Dictionary<string, IObjectPool<Projectile>> projectiles = new Dictionary<string, IObjectPool<Projectile>>();
    public Projectile GetProjectiles(string path, Vector3 _startPos, Vector3 target, float duration)
    {
        Projectile projectile = null;
        if (projectiles.ContainsKey(path))
        {
            projectile = projectiles[path].Get();
            projectile.SetTarget(_startPos, target, duration, path);
            return projectile;
        }

        projectile = Managers.Resource.Load<Projectile>("MonsterProjectile/" + path);
        MakeObjectPool(projectile, path);
        projectile = projectiles[path].Get();
        projectile.SetTarget(_startPos, target, duration, path);
        return projectile;
    }

    #region CreatePool
    public static void ProjectileReturnToPool(string path, Projectile projectile)
    {
        projectiles[path].Release(projectile);
    }

    private bool collectionChecks = true;
    private int maxPoolSize = 10;

    private void MakeObjectPool(Projectile prefab, string path)
    {
        if (projectiles.ContainsKey(path))
        {
            Debug.LogWarning($"Pool for {prefab.name} already exists.");
            return;
        }

        IObjectPool<Projectile> pool = new ObjectPool<Projectile>(
            () => CreateObject(prefab),
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionChecks,
            10,
            maxPoolSize
        );

        projectiles.Add(path, pool);
    }

    private Projectile CreateObject(Projectile prefab)
    {
        Projectile projectile = Managers.Resource.Instantiate(prefab);
        projectile.transform.parent = Managers.Instance.game.transform;
        return projectile;
    }

    private void OnTakeFromPool(Projectile obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Projectile obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Projectile obj)
    {
        Managers.Resource.Destroy(obj.gameObject);
    }

    #endregion

    private void Start()
    {
        playerPos = Camera.main.transform.position;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        currentHP = monsterData.hp;
    }

    public bool CanAttack()
    {
        if (Vector3.Distance(transform.position, playerPos) <= monsterData.attackRange)
        {
            return true;
        }
        return false;
    }

    public void LerpRotate(Vector3 targetPos, TweenCallback callback, float duration)
    {
        Vector3 directionToTarget = targetPos - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        Vector3 targetEulerAngles = targetRotation.eulerAngles;

        transform.DORotate(targetEulerAngles, duration).SetEase(Ease.Linear).OnComplete(callback);
    }

    public void Attack()
    {
        if (monsterData.attackRange >= 20)
        {
            GetProjectiles(monsterData.projectile, firePos.position, playerPos, 3f);
            //Projectile newProjectile = Instantiate(projectile);
            //newProjectile.transform.parent = Managers.Instance.game.transform;
            //newProjectile.SetTarget(firePos.position, playerPos, 3f);
            //newProjectile.SetDamage(monsterData.attackDamage);
        }
        else
        {
            // 근접공격 로직
            // 플레이어에게 데미지를 주는 코드를 작성
            GameManager.Instance.GetDamage(monsterData.attackDamage);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("Chase", true);
    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            animator.SetTrigger("Dead");
            GameManager.Instance.GetGold(monsterData.gold);
        }
    }
}
