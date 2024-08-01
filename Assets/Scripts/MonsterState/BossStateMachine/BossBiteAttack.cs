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


        targetPosition = PlayerBackVector3(currentPosition, bossMonster.playerPos, 20f);
        bossMonster.transform.DOLookAt(targetPosition, rotateDuration);
        moveTween = bossMonster.transform.DOMove(targetPosition, moveDuration);
        bossMonster.bossAudio.PlayOneShot(Managers.SoundManager.GetAudioClip(bossMonster.bossData.sound));

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 2.5f)
        {
            GameManager.Instance.GetDamage(bossMonster.bossData.attackDamage);
            animator.SetBool("BossIdle", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

    Vector3 PlayerBackVector3(Vector3 enemyPosition, Vector3 playerPosition, float distance)
    {
        Vector3 targetOffset = playerPosition + new Vector3(0, -5, 0);

        Vector3 direction = (targetOffset - enemyPosition).normalized;

        Vector3 newPosition = targetOffset + direction * distance;

        return newPosition;
    }
}
