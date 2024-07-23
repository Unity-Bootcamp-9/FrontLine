using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    private Weapon currentWeapon;
    private StageData currentStage;
    private List<Portal> portals;

    private int currentHP;
    private int gameTimer;
    private int score;

    private readonly int MaxHP = 100;
    private readonly int MaxPlayTime = 90;

    private void Start()
    {
        portals = new List<Portal>();
        player = Camera.main.transform.GetChild(0).transform;
    }

    public void Initialize(StageData stageData)
    {
        currentHP = MaxHP;
        gameTimer = MaxPlayTime;
        SetWeapon(Managers.DataManager.weaponDatas[0]); // 나중에 무기 선택 UI에서 실행
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        while (gameTimer > 0)
        {
            if (gameTimer % 10 == 0)
            {
                Portal portal = new GameObject("Portal").AddComponent<Portal>();
                portal.Initialize(currentStage.monsterIndexs, currentStage.spawnDelays);
                portals.Add(portal);
            }
            yield return new WaitForSeconds(1f);
            gameTimer -= 1;
        }
        Win();
    }

    private void SetWeapon(WeaponData weaponData)
    {
        string path = weaponData.weaponPrefab;

        Weapon gunPrefab = Managers.Resource.Load<Weapon>(path);

        if (gunPrefab != null)
        {
            Weapon newGun = Instantiate(gunPrefab);

            newGun.Initialize(weaponData);

            newGun.name = weaponData.weaponName;

            newGun.transform.parent = player;
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;
            currentWeapon = newGun;
        }
        else
        {
            Debug.LogError("Failed to load gun prefab.");
        }
    }

    public bool WeaponIsNotNull()
    {
        return currentWeapon != null;
    }

    public void GetDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0)
            Dead();
    }

    public void GetScore(int score)
    {
        this.score += score;
    }

    private void Dead()
    {
        for(int i = 0; i < portals.Count; i++)
        {
            portals[i].StopSpawn();
        }
        portals.Clear();

        // 게임오버 UI 불러와 게임 오버 처리하기 
    }

    private void Win()
    {
        for (int i = 0; i < portals.Count; i++)
        {
            portals[i].StopSpawn();
        }
        portals.Clear();

        // 게임 승리 UI 불러와 게임 승리 처리하기
    }
}
