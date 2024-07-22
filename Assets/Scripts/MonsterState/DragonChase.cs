using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DragonChase : StateMachineBehaviour
{
    private Vector3 playerPos;
    private Monster monster;        

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = Camera.main.transform.position;
        monster = animator.GetComponent<Monster>();
        monster.SetVelocityMoveSpeed();
        monster.LerpRotate(playerPos, () => monster.SetVelocityMoveSpeed(), 1f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(monster.CanAttack(playerPos))
        {
            monster.SetAnimator("CanAttack", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster.SetAnimator("Chase", false);
        monster.SetVelocityZero();
    }
}
