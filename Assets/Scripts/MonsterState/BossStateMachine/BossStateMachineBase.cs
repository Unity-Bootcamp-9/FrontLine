using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BossStateMachineBase : StateMachineBehaviour
{
    protected Monster monster;
    protected Vector3 currentPosition;
    protected Vector3 targetPosition;
    protected float timer;
    protected string animationName;

    virtual public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(monster == null)
        {
            monster = animator.GetComponent<Monster>();
        }

        animationName = this.GetType().Name;
        currentPosition = monster.transform.position;
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
