using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public Vector3 playerPos;
    public Rigidbody rigidBody;
    public BossData bossData;
    public Transform fireballOffset;
    public Transform bigFireballOffset;


    public void Initalize(BossData _bossData)
    {
        bossData = _bossData;
    }

    private void Start()
    {
        playerPos = Camera.main.transform.position;
        rigidBody = GetComponent<Rigidbody>();
        bossData = Managers.DataManager.bossDatas[0];
    }


}