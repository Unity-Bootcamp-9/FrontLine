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

        bossMonster.transform.DOMove(targetPosition, duration);
        bossMonster.transform.DOLookAt(targetPosition, 0.2f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(distance < 10f)
        {
            animator.SetBool("BossToPlayer", false);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
