using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public WeaponData weaponData;
    
    private void Start()
    {
        player = Camera.main.transform.GetChild(0).gameObject;
    }

    private void Update()
    {

    }

    int value = -1;
    public void ChangeValue()
    {
        value++;

        if(value > 1) 
        {
            value = 0;
        }

        Debug.Log(value);
    }
    public void GetData()
    {
        if(Managers.DataManager.weaponDatas.Count == 0)
        {
            Debug.LogError("No WeaponData");
        }
        else
        {
            weaponData = Managers.DataManager.weaponDatas[value];
        }
    }

    public void SetWeapon()
    {
        GetData();

        string path = weaponData.weaponPrefab;

        GameObject gunPrefab = Resources.Load<GameObject>(path);

        if (gunPrefab != null)
        {
            GameObject newGun = Instantiate(gunPrefab);

            Weapon weapon = newGun.GetComponent<Weapon>();

            weapon.Initialize(weaponData);

            newGun.name = weaponData.weaponName;

            newGun.transform.parent = player.transform; 
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("Failed to load gun prefab.");
        }
    }
}
