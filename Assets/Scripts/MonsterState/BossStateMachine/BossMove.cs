using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : BossStateMachineBase
{
    int nextMove;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Vector3 randomDirection = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.1f, 0.1f), Random.Range(-0.3f, 0.3f)).normalized;
        float moveDistance = 10f;
        float rotateDuration = 0.2f;
        float moveDuration = 2f;

        nextMove = Random.Range(1, 5);


        targetPosition = bossMonster.transform.position + randomDirection * moveDistance;
        bossMonster.transform.DOLookAt(targetPosition, rotateDuration);
        bossMonster.transform.DOMove(targetPosition, moveDuration);

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        float distance = Vector3.Distance(playerPosition, currentPosition);

        if (timer > 3f)
        {
            switch (nextMove)
            {
                case 1: animator.SetBool("BossFireballAttack", true); break;
                case 2: animator.SetBool("BossMoveAttack", true); break;
                case 3: animator.SetBool("BossBigFireball", true); break;
                case 4: animator.SetBool("BossBiteAttack", true); break;
                default: break;
            }
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
