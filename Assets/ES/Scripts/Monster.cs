using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Animator animator;
    public MonsterData monsterData;
    private Rigidbody rigidbody;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        monsterData.SetData("Dragon", 100, 5, 5, 20, "q", "w");
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
        if(Vector3.Distance(transform.position, targetPos) <= monsterData.attackRange)
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
        Debug.Log("АјАн");
    }

}
