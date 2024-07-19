using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DragonMove : StateMachineBehaviour
{
    private Monster monster;
    private Vector3 moveVector;
    private float timeCount;
    private float moveDuration;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.GetComponent<Monster>();
        moveVector = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.1f, 0.1f), Random.Range(-0.3f, 0.3f));
        timeCount = 0f;
        moveDuration = Random.Range(0.5f, 1.5f);
        monster.LerpRotate(monster.transform.position + moveVector, () => monster.SetVelocityMoveSpeed(), 1f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(moveDuration <= timeCount)
        {
            monster.SetAnimator("Chase", true);
        }
        timeCount += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster.SetVelocityZero();
        monster.SetAnimator("Move", false);
    }
}
