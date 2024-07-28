using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : BossStateMachineBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Vector3 randomDirection  = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.1f, 0.1f), Random.Range(-0.3f, 0.3f)).normalized;
        float moveDistance = 50f;
        float rotateDuration = 0.5f;
        float moveDuration = 5f;
        
        targetPosition  = bossMonster.transform.position + randomDirection * moveDistance;
        bossMonster.transform.DOLookAt(targetPosition, rotateDuration);
        bossMonster.transform.DOMove(targetPosition, moveDuration);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 5)
        {
            animator.SetBool("BossFireballAttack", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

}
