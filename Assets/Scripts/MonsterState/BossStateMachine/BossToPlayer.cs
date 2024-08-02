using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossToPlayer : BossStateMachineBase
{
    int nextMove;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        targetPosition = bossMonster.playerPos;

        float duration = 3f;

        bossMonster.transform.DOLookAt(targetPosition, 0.2f);
        moveTween = bossMonster.transform.DOMove(targetPosition, duration);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(timer > 1)
        {
            animator.SetBool("BossToPlayer", false);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
