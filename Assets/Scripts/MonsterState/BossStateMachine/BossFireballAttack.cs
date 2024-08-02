using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BossFireballAttack : BossStateMachineBase
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        moveTween = bossMonster.transform.DOLookAt(bossMonster.playerPos, 1f);

        GameObject fireball = Managers.Resource.Instantiate(bossMonster.fireball);

        bossMonster.bossAudio.PlayOneShot(Managers.SoundManager.GetAudioClip(bossMonster.bossData.sound));
        SetProjectile(fireball, bossMonster.bossData.attackDamage).SetTarget(bossMonster.fireballOffset.position, bossMonster.playerPos, 3f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        Debug.Log(timer);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 4f)
        {
            animator.SetBool("BossIdle", true);
        }
    }
}
