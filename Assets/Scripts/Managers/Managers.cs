using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    private static FirebaseManager firebaseManager = new FirebaseManager();
    private static DataManager dataManager = new DataManager();


    public List<MonsterData> monsterDatas;
    public List<WeaponData> weaponDatas;

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
    public static FirebaseManager FirebaseManager { get { return firebaseManager; } }
    public static DataManager DataManager { get { return dataManager; } }

    public void LoadTest()
    {
        FirebaseManager.GetDataFromTable("Weapon", dataManager.LoadData<WeaponData>);
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

        Initialize();
        StartCoroutine(GetData());
    }
    IEnumerator GetData()
    {
        yield return new WaitForSeconds(5f);
        monsterDatas = DataManager.monsterDatas;
        weaponDatas = DataManager.weaponDatas;
    }

    public void Initialize()
    {
        firebaseManager.Initialize(() => dataManager.Initialize());
    }
}
