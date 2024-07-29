using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BossStateMachineBase : StateMachineBehaviour
{
    protected BossMonster bossMonster;
    protected Vector3 currentPosition;
    protected Vector3 playerPosition;
    protected Vector3 targetPosition;
    protected float timer;
    protected string animationName;

    virtual public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(bossMonster == null)
        {
            bossMonster = animator.GetComponent<BossMonster>();
        }

        animationName = this.GetType().Name;
        currentPosition = bossMonster.transform.position;
        playerPosition = bossMonster.playerPos;
        timer = 0f;
    }

    virtual public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
    }

    virtual public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(animationName, false);
    }
}
