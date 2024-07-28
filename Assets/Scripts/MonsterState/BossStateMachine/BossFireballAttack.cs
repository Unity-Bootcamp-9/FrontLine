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

        float angleStep = 360f / fireballCount;
        float radius = 5f;

        for (int i = 0; i < fireballCount; i++)
        {
            GameObject fireball = Managers.Resource.Instantiate("MonsterProjectile/bossAttack", bossMonster.transform);

            fireballs.Add(fireball);

            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * radius;
            float y = Mathf.Sin(radians) * radius;

            // 파이어볼의 위치 설정
            fireball.transform.localPosition = new Vector3(x, y, 0);

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

        if (timer > 8f)
        {
            foreach (var fireball in fireballs)
            {
                fireball.transform.DOMove(playerPosition, 2f);
            }
            animator.SetBool("BossIdle", true);
        }
    }
}
