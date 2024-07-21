using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [Header("DataField")]
    [SerializeField] private WeaponData weaponData;
    private WaitForSeconds waitForFireDelay;
    private Method currentMethod;

    [SerializeField] private Transform player;
    [SerializeField] private Transform gunMuzzle;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isReadyToFire = false;

    private enum Method
    {
        hitScan = 1,
        projectile = 2
    }

    [Header("TestField")]
    [SerializeField] public GameObject bulletPrefab;
    private ObjectPool<GameObject> bulletPool;

    private void Awake()
    {
        player = Camera.main.transform;
        animator = GetComponent<Animator>();

        bulletPool = new ObjectPool<GameObject>(
            createFunc: CreateBullet,
            actionOnGet: ActivateBullet,
            actionOnRelease: DeactivateBullet,
            actionOnDestroy: DestroyBullet,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    public void Initialize(WeaponData weapon)
    {
        weaponData = weapon;
        bulletPrefab = Resources.Load<GameObject>(weaponData.bulletPrefab);

        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab을 로드할 수 없습니다: " + weaponData.bulletPrefab);
            return;
        }

        waitForFireDelay = new WaitForSeconds(weaponData.fireDelay);
        currentMethod = (Method)weaponData.method;
        isReadyToFire = true;
    }

    private void Update()
    {
        // 테스트용도
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Fire();
        }

        Debug.Log(weaponData.method + "method");
        Debug.Log(weaponData.bulletPrefab +"prefab");
        Debug.Log(weaponData.attackDamage + "Dmg");
    }

    private void Fire()
    {
        if (isReadyToFire)
        {
            StartCoroutine(FireCoroutine());
        }
    }

    private IEnumerator FireCoroutine()
    {
        isReadyToFire = false;

        animator.Play("Shot", 0, 0);

        GameObject bullet = bulletPool.Get();

        if (currentMethod == Method.hitScan)
        {
            RaycastHit hit;

            if (Physics.Raycast(player.position, player.forward, out hit, 20f))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }

        Handheld.Vibrate();

        yield return waitForFireDelay;

        isReadyToFire = true;
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<WeaponProjectile>().Initialize(bulletPool, weaponData.bulletSpeed);
        return bullet;
    }

    private void ActivateBullet(GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.transform.position = gunMuzzle.position;
        bullet.transform.rotation = Quaternion.LookRotation(gunMuzzle.forward);
    }

    private void DeactivateBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void DestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }

    public void Reload()
    {
        // Reload logic
    }
}