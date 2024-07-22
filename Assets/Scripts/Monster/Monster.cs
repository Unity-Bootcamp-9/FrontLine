using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Animator animator;
    public MonsterData monsterData;
    private Rigidbody rigidbody;
    private Projectile projectile;
    [SerializeField]
    private Transform firePos;

    private void Start()
    {
        projectile = Resources.Load<Projectile>(monsterData.projectile);
    }
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void SetVelocityMoveSpeed()
    {
        rigidbody.velocity = transform.forward * monsterData.moveSpeed;
    }

    public void SetVelocityZero()
    {
        rigidbody.velocity = Vector3.zero;
    }

    public void LookAt(Vector3 pos)
    {
        transform.LookAt(pos);
    }

    public void SetAnimator(string parameter, bool value)
    {
        animator.SetBool(parameter, value);
    }

    public bool CanAttack(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) <= monsterData.attackRange)
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

    public void Attack(Vector3 target)
    {
        if (monsterData.attackRange >= 20)
        {
            Projectile newProjectile = Instantiate(projectile);
            newProjectile.SetTarget(firePos.position, target);
        }
        else
        {
            // 근접공격 로직
            // 플레이어에게 데미지를 주는 코드를 작성

        }
    }

    private void Update()
    {
        Debug.Log(name + rigidbody.velocity);
    }

}
