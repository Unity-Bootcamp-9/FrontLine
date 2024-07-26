using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BiteAttack : BossStateMachineBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        targetPosition = MoveToPlayerBack(currentPosition, monster.playerPos, 10f);

        monster.transform.DOMove(targetPosition, 1.5f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
        if(Vector3.Distance(monster.transform.position, targetPosition) < 0.2f)
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
