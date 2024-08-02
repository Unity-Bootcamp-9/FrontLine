using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class BossStateMachineBase : StateMachineBehaviour
{
    protected BossMonster bossMonster;
    protected Vector3 currentPosition;
    protected Vector3 playerPosition;
    protected Vector3 targetPosition;
    protected Tween moveTween;
    protected float timer;
    protected float distance;
    protected string animationName;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.TryGetComponent<BossMonster>(out BossMonster boss))
        {
            bossMonster = boss;
        }

        if (bossMonster == null)
        {
            return;
        }

        animationName = this.GetType().Name;
        currentPosition = bossMonster.transform.position;
        playerPosition = bossMonster.playerPos;
        timer = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(bossMonster.currentHp <= 1)
        {
            animator.SetBool("BossDie", true);
        }

        timer += Time.deltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveTween.Kill();
        animator.SetBool(animationName, false);
    }

    protected Projectile SetProjectile(GameObject fireball, int damage)
    {
        Projectile fireballProjectile = fireball.GetComponent<Projectile>();
        fireballProjectile.SetDamage(damage);
        return fireballProjectile;
    }
}
