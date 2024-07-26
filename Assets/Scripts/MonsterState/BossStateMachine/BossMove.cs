using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : BossStateMachineBase
{
    private int nextAttack;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        nextAttack = Random.Range(0, 2);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(timer > 3)
        {
            animator.SetBool("BiteAttack", true);
            //switch (nextAttack)
            //{
            //    case 0:
            //        animator.SetBool("BiteAttack", true);
            //        break;
            //    case 1:
            //        animator.SetBool("StingerAttack", true);
            //        break;
            //}
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
