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
    private bool isDead;
    public int currentStageGold {  get; private set; }
    public bool gameClear { get; private set; }

    public delegate void HPChanged(int currentHP);
    public event HPChanged OnHPChanged;

    private readonly int MaxHP = 100;
    private readonly int MaxPlayTime = 90;
    private readonly float PortalSpawnTime = 10f;
    private readonly float MaxXZ = 24f;
    private const float yOffset = -2f;

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

    public void Initialize(StageData stageData)
    {
        currentStage = stageData;
        currentHP = MaxHP;
        gameTimer = MaxPlayTime;
        currentStageGold = 0;
        gameClear = false;
        isDead = false;
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
                portal.transform.parent = Managers.Instance.game.transform;
                portal.transform.position = new Vector3(Random.Range(-MaxXZ, MaxXZ), yOffset, Random.Range(-MaxXZ, MaxXZ));
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

    public void GenerateBoss()
    {
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
        OnHPChanged?.Invoke(currentHP);
    }

    public void GetGold(int gold)
    {
        this.currentStageGold += gold;
    }

    private void Dead()
    {
        if (isDead)
            return;
        isDead = true;
        for (int i = 0; i < portals.Count; i++)
        {
            portals[i].StopSpawn();
            portals[i].ResetPools();
            Destroy(portals[i].gameObject);
        }
        portals.Clear();
        StopAllCoroutines();
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_GameEndPopUp>();
    }

    private void Win()
    {
        for (int i = 0; i < portals.Count; i++)
        {
            portals[i].StopSpawn();
            portals[i].ResetPools();
        }
        portals.Clear();

        StopCoroutine(GameStart());
        Destroy(Managers.Instance.game);
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_GameEndPopUp>();
    }
}
