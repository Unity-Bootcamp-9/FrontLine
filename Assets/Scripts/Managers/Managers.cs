using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    private static FirebaseInitializer firebaseInitializer = new FirebaseInitializer();
    private static FirebaseManager firebaseManager = new FirebaseManager();
    private static DataManager dataManager = new DataManager();
    private static UIManager uiManager = new UIManager();
    private static ResourceManager resource = new ResourceManager();
    private static EffectManager effectManager = new EffectManager();
    private static SoundManager soundManager = new SoundManager();
    private static ARManager arManager = new ARManager();

    public GameObject game;

    public List<MonsterData> monsterDatas;
    public List<WeaponData> weaponDatas;
    public List<StageData> stageDatas;
    public List<BossData> bossDatas;


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
    public static FirebaseInitializer FirebaseInitializer { get { return firebaseInitializer; } }
    public static FirebaseManager FirebaseManager { get { return firebaseManager; } }
    public static DataManager DataManager { get { return dataManager; } }
    public static UIManager UI { get { return uiManager; } }
    public static ResourceManager Resource { get { return resource; } }
    public static EffectManager EffectManager { get { return effectManager; } }
    public static SoundManager SoundManager { get { return soundManager; } }
    public static ARManager ARManager { get { return arManager; } }


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

        firebaseInitializer.Init(Initialize);
        StartCoroutine(GetData());
    }
    IEnumerator GetData()
    {
        yield return new WaitForSeconds(2f);
        monsterDatas = DataManager.monsterDatas;
        weaponDatas = DataManager.weaponDatas;
        stageDatas = DataManager.stageDatas;
        bossDatas = DataManager.bossDatas;
    }

    public void Initialize()
    {
        firebaseManager.Initialize(() => dataManager.Initialize());
        effectManager.Init();
        soundManager.Init();
        Debug.Log("Init");
    }
    public static class GlobalSettings
    {
        public static bool isVibrateEnabled = true; // 기본값 설정
    }

    public static void SetVibrationONOFF(bool value)
    {
        if (value)
        {
            GlobalSettings.isVibrateEnabled = false;
        }
        else
            GlobalSettings.isVibrateEnabled = true;
    }

}
