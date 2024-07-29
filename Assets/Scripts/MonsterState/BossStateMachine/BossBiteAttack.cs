using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossBiteAttack : BossStateMachineBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        float rotateDuration = 0.5f;
        float moveDuration = 2f;

        targetPosition = MoveToPlayerBack(currentPosition, bossMonster.playerPos, 20f);

        bossMonster.transform.DOLookAt(targetPosition, rotateDuration);
        bossMonster.transform.DOMove(targetPosition, moveDuration);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
        if(timer > 3f)
        {
            animator.SetBool("BossIdle", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

    Vector3 MoveToPlayerBack(Vector3 enemyPosition, Vector3 playerPosition, float distance)
    {
        Vector3 direction = (playerPosition - enemyPosition).normalized;

        Vector3 newPosition = playerPosition + direction * distance;

        return newPosition;
    }
}
