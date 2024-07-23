using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDead : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Monster myMonster = animator.GetComponent<Monster>();
        myMonster.portal.ReturnToPool(myMonster, myMonster.monsterData.index);
        animator.SetBool("CanAttack", false);
        animator.SetBool("Move", false);
        animator.SetBool("Chase", false);
    }
}
