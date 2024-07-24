using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DragonAttack : StateMachineBehaviour
{
    private Monster monster;
    private int frame;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monster == null)
            monster = animator.GetComponent<Monster>();
        animator.SetBool("Move", true);
        animator.transform.LookAt(Camera.main.transform.position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        frame++;
        if(frame == 21)
            monster.Attack();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("CanAttack", false);
        frame = 0;
    }

}
