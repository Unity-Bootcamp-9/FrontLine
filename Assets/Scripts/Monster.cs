using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Chase, Attack, Move, Dead
}

public struct MonsterData
{
    public string name { get; private set; }
    public float hp { get; private set; }
    public float moveSpeed { get; private set; }
    public float attackDamage {  get; private set; }
    public string projectile { get; private set; }
    public string sound {  get; private set; }

    public void SetData()
    {
        name = string.Empty;
    }
}

public class Monster : MonoBehaviour
{
    private MonsterState nowState;
    private Animator animator;
    private MonsterData monsterData;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (nowState)
        {
            case MonsterState.Chase:
                break;
            case MonsterState.Attack:
                break;
            case MonsterState.Move:
                break;
            case MonsterState.Dead:
                break;
        }
    }
}
