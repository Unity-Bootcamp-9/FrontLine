using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBigFireball : BossStateMachineBase
{
    GameObject fireball;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        targetPosition = Camera.main.transform.position;

        bossMonster.transform.DOLookAt(targetPosition, 0.5f);

        fireball = Managers.Resource.Instantiate("MonsterProjectile/bossFireball");

        fireball.transform.position = bossMonster.bigFireballOffset.position;
        fireball.transform.localScale = Vector3.one;

        fireball.transform.DOScale(Vector3.one * 3, 3f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 3)
        {
            fireball.transform.DOMove(bossMonster.playerPos, 1.5f).SetEase(Ease.InOutQuad);
        }

        if(timer > 5)
        {
            animator.SetBool("BossIdle", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
