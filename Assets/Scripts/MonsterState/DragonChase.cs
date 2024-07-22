using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DragonChase : StateMachineBehaviour
{
    private Monster monster;
    private Rigidbody rigidbody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(rigidbody == null)
            rigidbody = animator.GetComponent<Rigidbody>();
        if(monster == null)
            monster = animator.GetComponent<Monster>();

        monster.LerpRotate(Camera.main.transform.position, () =>
        rigidbody.velocity = monster.transform.forward * monster.monsterData.moveSpeed, 1f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(monster.CanAttack())
        {
            animator.SetBool("CanAttack", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Chase", false);
        rigidbody.velocity = Vector3.zero;
    }
}
