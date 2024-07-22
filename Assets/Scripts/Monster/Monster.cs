using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Vector3 playerPos;
    private Animator animator;
    public MonsterData monsterData;
    private Projectile projectile;
    [SerializeField]
    private Transform firePos;

    private void Start()
    {
        playerPos = Camera.main.transform.position;
        projectile = Resources.Load<Projectile>(monsterData.projectile);
        animator = GetComponent<Animator>();
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
        }
        else
        {
            // 근접공격 로직
            // 플레이어에게 데미지를 주는 코드를 작성

        }
    }

}
