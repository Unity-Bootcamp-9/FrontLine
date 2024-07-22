using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public List<MonsterData> monsterDatas;
    public List<WeaponData> weaponDatas;

    private static Managers _instance;

    private static FirebaseManager firebaseManager = new FirebaseManager();
    private static DataManager dataManager = new DataManager();
    private static UIManager uiManager = new UIManager();

    public static FirebaseManager FirebaseManager { get { return firebaseManager; } }
    public static DataManager DataManager { get { return dataManager; } }
    public static UIManager UI { get { return uiManager; } }

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

        Initialize();

        StartCoroutine(Test());
    }

    public void Initialize()
    {
        firebaseManager.Initialize(() => dataManager.Initialize());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(2f);
        monsterDatas = dataManager.monsterDatas;
        weaponDatas = dataManager.weaponDatas;
    }
}
