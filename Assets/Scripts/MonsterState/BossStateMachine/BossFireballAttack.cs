using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BossFireballAttack : BossStateMachineBase
{
    private int fireballCount;
    private List<GameObject> fireballs = new List<GameObject>();

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        fireballCount = Random.Range(5, 10);

        bossMonster.transform.DOLookAt(bossMonster.playerPos, 1f);

        for(int i = 0; i < fireballCount; i++)
        {
            GameObject fireball = Managers.Resource.Instantiate("MonsterProjectile/bossFireball");

            fireball.transform.position = bossMonster.fireballOffset.position;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        Debug.Log(timer);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 6f)
        {
            animator.SetBool("BossIdle", true);
        }
    }
}
