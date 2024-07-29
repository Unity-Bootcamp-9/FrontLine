using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShortFireball : BossStateMachineBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        targetPosition = Camera.main.transform.position;

        bossMonster.transform.DOLookAt(targetPosition, 0.5f);

        GameObject fireball = Managers.Resource.Instantiate("MonsterProjectile/bossFireball");
        
        fireball.transform.position = bossMonster.fireballOffset.position;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 1f)
        {
            animator.SetBool("NextAction", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetBool("NextAction", false);
    }

}
