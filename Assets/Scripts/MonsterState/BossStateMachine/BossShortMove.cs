using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShortMove : BossStateMachineBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Vector3 randomDirection = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.1f, 0.1f), Random.Range(-0.2f, 0.2f)).normalized;
        float moveDistance = 10;
        float rotateDuration = 0.1f;
        float moveDuration = 1f;

        targetPosition = bossMonster.transform.position + randomDirection * moveDistance;
        bossMonster.transform.DOLookAt(targetPosition, rotateDuration);
        bossMonster.transform.DOMove(targetPosition, moveDuration);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 1)
        {
            animator.SetBool("NextAction", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("NextAction", false);
    }

}
