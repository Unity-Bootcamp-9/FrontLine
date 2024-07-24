using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public Transform player;
    private Weapon currentWeapon;
    private StageData currentStage;
    private List<Portal> portals;

    private int currentHP;
    private float gameTimer;
    private int score;

    private readonly int MaxHP = 100;
    private readonly int MaxPlayTime = 90;
    private readonly float PortalSpawnTime = 10f;
    private readonly float MaxXZ = 40f;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<GameManager>();
                singletonObject.name = typeof(GameManager).ToString();

                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        portals = new List<Portal>();
        player = Camera.main.transform.GetChild(0);
    }

    public void Initialize(StageData stageData)
    {
        currentStage = stageData;
        score = 0;
        currentHP = MaxHP;
        gameTimer = MaxPlayTime;
        StopCoroutine(GameStart());
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        float portalSpawnTimer = 0f;
        while (gameTimer > 0)
        {
            if (portalSpawnTimer <= 0)
            {
                Portal portal = new GameObject("Portal").AddComponent<Portal>();
                portal.Initialize(currentStage.monsterIndexs, currentStage.spawnDelays);
                portal.transform.position = new Vector3(Random.Range(-MaxXZ, MaxXZ), 0, Random.Range(-MaxXZ, MaxXZ));
                portals.Add(portal);
                portalSpawnTimer = PortalSpawnTime;
            }
            yield return null;
            gameTimer -= Time.deltaTime;
            portalSpawnTimer -= Time.deltaTime;
        }
        Win();
    }

    public void SetWeapon(WeaponData weaponData)
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

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
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
        Debug.Log("피해 입음");
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

        StopCoroutine(GameStart());
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
