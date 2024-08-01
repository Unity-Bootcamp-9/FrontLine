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
    protected float timer;
    protected float distance;
    protected string animationName;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(bossMonster == null)
        {
            bossMonster = animator.GetComponent<BossMonster>();
        }

        animationName = this.GetType().Name;
        playerPosition = bossMonster.playerPos;
        timer = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        currentPosition = bossMonster.transform.position;

        distance = Vector3.Distance(currentPosition, bossMonster.playerPos);

        float maxDistance = 50;

        if(distance > maxDistance)
        {
            animator.SetBool("BossToPlayer", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(animationName, false);
    }

    protected Projectile SetProjectile(GameObject fireball, int damage)
    {
        Projectile fireballProjectile = fireball.GetComponent<Projectile>();
        fireballProjectile.SetDamage(damage);
        return fireballProjectile;
    }
}
