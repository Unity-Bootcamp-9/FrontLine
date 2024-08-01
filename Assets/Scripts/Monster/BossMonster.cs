using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour, IMonster
{
    public BossData bossData;
    public Vector3 playerPos;
    public Transform fireballOffset;
    public Transform bigFireballOffset;
    public int currentHp;
    public GameObject fireball;
    public Action<int> OnHpChanged;

    public void GetDamage(int _damage)
    {
        Debug.Log("hit");
        Debug.Log(_damage);
        if (currentHp > 0)
        {
            currentHp -= _damage;
            OnHpChanged(_damage);
        }

        else if (currentHp <= 0) 
        {
            GameManager.Instance.gameClear = true;
            GameManager.Instance.GameOver();
            Destroy(this.gameObject);
        }

    }

    public void Initalize(BossData _bossData)
    {
        bossData = _bossData;
        currentHp = bossData.hp;
        fireball = Managers.Resource.Load<GameObject>($"MonsterProjectile/{bossData.projectile}");
        playerPos = Camera.main.transform.position;
        //gameObject.transform.parent = Managers.Instance.game.transform;
    }
}