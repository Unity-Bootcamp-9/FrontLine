using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public WeaponData weaponData;
    private Weapon currentWeapon;

    private int hp;
    private float gameTimer;
    private int score;
    
    private void Start()
    {
        player = Camera.main.transform.GetChild(0).transform;
    }

    public void Initialize()
    {
        hp = 100;
        gameTimer = 90f;
    }

    private void Update()
    {

    }

    public void SelectWeapon(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        SetWeapon();
    }

    public void SetWeapon()
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

    public bool WeaponIsNotNull()
    {
        return currentWeapon != null;
    }

    private void Dead()
    {
        // 게임오버 UI 불러와 게임 오버 처리하기 
    }

    private void Win()
    {
        // 게임 승리 UI 불러와 게임 승리 처리하기
    }
}
