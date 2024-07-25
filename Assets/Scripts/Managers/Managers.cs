using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    //private static FirebaseManager firebaseManager = new FirebaseManager();
    private static DataManager dataManager = new DataManager();
    //private static SpawnManager spawnManager = new SpawnManager();
    private static UIManager uiManager = new UIManager();
    private static ResourceManager resource = new ResourceManager();
    private static EffectManager effectManager = new EffectManager();
    private static SoundManager soundManager = new SoundManager();

    public List<MonsterData> monsterDatas;
    public List<WeaponData> weaponDatas;
    public List<StageData> stageDatas;

    public static Managers Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<Managers>();
                singletonObject.name = typeof(Managers).ToString();

                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }
    //public static FirebaseManager FirebaseManager { get { return firebaseManager; } }
    public static DataManager DataManager { get { return dataManager; } }
    //public static SpawnManager SpawnManager { get { return spawnManager; } }
    public static UIManager UI { get { return uiManager; } }
    public static ResourceManager Resource { get { return resource; } }
    public static EffectManager EffectManager { get { return effectManager; } }
    public static SoundManager SoundManager { get { return soundManager; } }


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

        Initialize();
        StartCoroutine(GetData());
    }
    IEnumerator GetData()
    {
        yield return new WaitForSeconds(2f);
        monsterDatas = DataManager.monsterDatas;
        weaponDatas = DataManager.weaponDatas;
        stageDatas = DataManager.stageDatas;
    }

    // 파이어베이스 연결되면 사용 예정 
    //public void Initialize() 
    //{
    //    firebaseManager.Initialize(() => dataManager.Initialize());
    //    spawnManager.Initialize();
    //}

    public void Initialize()
    {
        dataManager.Initialize();
        effectManager.Init();
        soundManager.Init();
        Debug.Log("Init");
    }
}
