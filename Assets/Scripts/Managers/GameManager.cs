using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    private Weapon currentWeapon;
    public StageData currentStage { get; private set; }
    private List<Portal> portals;

    public int currentHP;
    private float gameTimer;
    private bool alive;
    public int currentStageGold {  get; private set; }
    public bool gameClear { get; set; }

    public delegate void HPChanged(int currentHP);
    
    public event HPChanged OnHPChanged;

    public Action OnBossMonsterAppear;

    private readonly int MaxHP = 100;
    private readonly int MaxPlayTime = 60;
    private readonly float PortalSpawnTime = 10f;
    private readonly float MaxXZ = 24f;
    private const float yOffset = -2f;

    public static bool muteVibration = false; // 기본값 설정

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

    private void Awake()
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
    }

    public void Initialize()
    {
        currentHP = MaxHP;
        gameTimer = MaxPlayTime;
        currentStageGold = 0;
        gameClear = false;
        alive = true;
        StopCoroutine(GameStart());
        StartCoroutine(GameStart());
    }

    public void SelectStage(StageData stageData)
    {
        currentStage = stageData;
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
                portal.transform.parent = Managers.Instance.game.transform;
                portal.transform.position = new Vector3(UnityEngine.Random.Range(-MaxXZ, MaxXZ), yOffset, UnityEngine.Random.Range(-MaxXZ, MaxXZ));
                portals.Add(portal);
                portalSpawnTimer = PortalSpawnTime;
            }
            yield return null;
            gameTimer -= Time.deltaTime;
            portalSpawnTimer -= Time.deltaTime;
        }

        GenerateBossMonster();
    }

    public BossMonster currentBoss;

    public void GenerateBossMonster()
    {
        int bossIndex = currentStage.bossIndex;
        string name = Managers.DataManager.bossDatas[bossIndex].name;
        GameObject bossObject = Managers.Resource.Instantiate($"Monster/Boss/{name}");
        bossObject.transform.parent = Managers.Instance.game.transform;
        
        currentBoss = bossObject.GetComponent<BossMonster>();
        currentBoss.Initalize(Managers.DataManager.bossDatas[bossIndex]);
        OnBossMonsterAppear?.Invoke();
    }

    public void SetWeapon(WeaponData weaponData)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        string path = weaponData.weaponPrefab;

        Weapon gunPrefab = Managers.Resource.Load<Weapon>(path);

        if (gunPrefab != null)
        {
            Weapon newGun = Instantiate(gunPrefab);

            newGun.Initialize(weaponData);

            newGun.name = weaponData.weaponName;

            newGun.transform.parent = Camera.main.transform.Find("handPivot");
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;
            currentWeapon = newGun;
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
        OnHPChanged?.Invoke(currentHP);

        if (currentHP < 0 && alive)
        {
            GameOver();
            alive = false;
        }
    }

    

    public void GetGold(int gold)
    {
        this.currentStageGold += gold;
    }

    public void GameOver()
    {
        ResetGame();
        Managers.UI.ShowPopupUI<UI_GameEndPopUp>();
    }

    public void ResetGame()
    {
        for (int i = 0; i < portals.Count; i++)
        {
            portals[i].StopSpawn();
            portals[i].ResetPools();
            Destroy(portals[i].gameObject);
        }
        OnBossMonsterAppear = null;

        if(currentBoss != null)
        {
            Destroy(currentBoss.gameObject);
        }

        portals.Clear();
        StopAllCoroutines();
        Managers.UI.ClosePopupUI();
        Managers.ARManager.SetARActive(false);
        Monster.ClearPool();
    }
}
