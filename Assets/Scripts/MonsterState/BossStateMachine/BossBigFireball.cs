using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBigFireball : BossStateMachineBase
{
    private GameObject fireball;
    private bool isShoot = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        isShoot = false;

        targetPosition = Camera.main.transform.position;

        bossMonster.transform.DOLookAt(targetPosition, 0.5f);

        fireball = Managers.Resource.Instantiate("MonsterProjectile/bossElectric");

        fireball.transform.position = bossMonster.bigFireballOffset.position;

        fireball.transform.localScale = Vector3.one;

        fireball.transform.DOScale(Vector3.one * 10f, 3f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timer > 3 && !isShoot)
        {
            Projectile projectile = fireball.GetComponent<Projectile>();

            projectile.SetDamage(bossMonster.bossData.attackDamage);
            projectile.SetTarget(bossMonster.bigFireballOffset.position, bossMonster.playerPos, 5f);

            isShoot = true;
        }

        if (timer > 5)
        {
            animator.SetBool("BossIdle", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
