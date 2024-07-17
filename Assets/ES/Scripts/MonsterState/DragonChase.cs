using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DragonChase : StateMachineBehaviour
{
    private Vector3 playerPos;
    private Monster monster;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = Camera.main.transform.position;
        monster = animator.GetComponent<Monster>();
        monster.LerpRotate(playerPos, () => monster.SetVelocityMoveSpeed(), 1f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(monster.CanAttack(playerPos))
        {
            monster.SetAnimator("CanAttack", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster.SetAnimator("Chase", false);
        monster.SetVelocityZero();
    }
}
