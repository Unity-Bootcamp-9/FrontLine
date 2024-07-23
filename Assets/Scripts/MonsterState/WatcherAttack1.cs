using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherAttack1 : StateMachineBehaviour
{
    private Monster monster;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monster == null)
            monster = animator.GetComponent<Monster>();
        monster.Attack();
    }

}
