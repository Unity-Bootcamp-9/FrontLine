using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShortFireball : BossStateMachineBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        targetPosition = Camera.main.transform.position;

        bossMonster.bossAudio.PlayOneShot(Managers.SoundManager.GetAudioClip(bossMonster.bossData.sound));
        moveTween = bossMonster.transform.DOLookAt(targetPosition, 0.5f);
        GameObject fireball = Managers.Resource.Instantiate(bossMonster.fireball);
        fireball.transform.position = bossMonster.fireballOffset.position;
        SetProjectile(fireball, bossMonster.bossData.attackDamage).SetTarget(bossMonster.fireballOffset.position, bossMonster.playerPos, 3f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 1f)
        {
            animator.SetBool("NextAction", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("NextAction", false);
    }

}
