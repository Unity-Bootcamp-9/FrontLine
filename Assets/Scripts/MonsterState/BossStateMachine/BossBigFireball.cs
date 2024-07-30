using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBigFireball : BossStateMachineBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        targetPosition = bossMonster.playerPos;

        bossMonster.transform.DOLookAt(targetPosition, 0.5f);

        GameObject fireball = Managers.Resource.Instantiate(bossMonster.fireball);

        fireball.transform.position = bossMonster.bigFireballOffset.position;
        
        fireball.transform.DOScale(Vector3.one * 10f, 3f).OnComplete(() => {
            Debug.Log(fireball.gameObject.name);
            SetProjectile(fireball, bossMonster.bossData.attackDamage).SetTarget(fireball.transform.position, targetPosition, 2f);
        }
        );
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 10)
        {
            animator.SetBool("BossIdle", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
