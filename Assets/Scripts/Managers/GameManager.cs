using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public WeaponData weaponData { get; set; }
    private Weapon currentWeapon;
    private int hp;
    
    private void Start()
    {
        player = Camera.main.transform.GetChild(0).transform;
    }

    public void Initialize()
    {
        hp = 100;
    }

    private void Update()
    {

    }


    public void SetWeapon()
    {
        string path = weaponData.weaponPrefab;

        Weapon gunPrefab = Resources.Load<Weapon>(path);

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
}
