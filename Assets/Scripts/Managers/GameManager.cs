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

    public void GetData()
    {
        if(Managers.Instance.weaponDatas.Count == 0)
        {
            Debug.LogError("No WeaponDatas");
        }
        else
        {
            weaponData = Managers.Instance.weaponDatas[0];
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
