using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDead : StateMachineBehaviour
{
    Monster myMonster;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(myMonster == null)
            myMonster = animator.GetComponent<Monster>();

        Managers.EffectManager.GetEffect("MonsterDead", animator.GetComponentInChildren<Collider>().transform.position, Quaternion.identity);
        animator.GetComponent<AudioSource>().PlayOneShot(Managers.SoundManager.GetAudioClip("MonsterDead"));
        foreach (Transform child in animator.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myMonster.portal.ReturnToPool(myMonster, myMonster.monsterData.index);
        animator.SetBool("CanAttack", false);
        animator.SetBool("Move", false);
        animator.SetBool("Chase", false);
    }
}
