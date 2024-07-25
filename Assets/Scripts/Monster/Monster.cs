using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Vector3 playerPos;
    private Animator animator;
    public MonsterData monsterData;
    public Portal portal;
    private Projectile projectile;
    private int currentHP;
    public Rigidbody rigidBody;
    //private Collider monsterCollider;
    [SerializeField] private Transform firePos;

    private void Start()
    {
        playerPos = Camera.main.transform.position;
        projectile = Resources.Load<Projectile>("MonsterProjectile/" + monsterData.projectile);
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        //monsterCollider = GetComponentInChildren<Collider>(); 
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
            Projectile newProjectile = Instantiate(projectile);
            newProjectile.SetTarget(firePos.position, playerPos);
            newProjectile.SetDamage(monsterData.attackDamage);
        }
        else
        {
            // 근접공격 로직
            // 플레이어에게 데미지를 주는 코드를 작성
            GameManager.Instance.GetDamage(monsterData.attackDamage);

        }
    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            animator.SetTrigger("Dead");
    }

    private void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("Chase", true);
    }
}
