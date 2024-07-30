using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    public BossData bossData;
    public Transform fireballOffset;
    public Transform bigFireballOffset;
    public GameObject fireball;

    public void Initalize(BossData _bossData)
    {
        bossData = _bossData;
        currentHP = bossData.hp;
        fireball = Managers.Resource.Load<GameObject>($"MonsterProjectile/{bossData.projectile}");
    }

    private void Update()
    {
        if(currentHP < 0)
        {
            GameManager.Instance.GameOver();
            GameManager.Instance.gameClear = true;
        }
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
    }
}