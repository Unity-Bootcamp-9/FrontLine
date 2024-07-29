using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public Vector3 playerPos;
    private Animator animator;
    public Rigidbody rigidBody;
    public BossData bossData;


    private void Start()
    {
        playerPos = Camera.main.transform.position;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        bossData = Managers.DataManager.bossDatas[0];
    }
}