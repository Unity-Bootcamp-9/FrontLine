using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("DataField")]
    [SerializeField] private WeaponData weaponData;

    [SerializeField] private WaitForSeconds waitForFireDelay;
    [SerializeField] private WaitForSeconds waitForReloadTime;
    [SerializeField] private Method currentMethod;
    [SerializeField] private int currentBulletsCount;
    [SerializeField] private Transform gunMuzzle;

    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isReadyToFire = true; 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ObjectPool<GameObject> bulletPool;

    private enum Method
    {
        hitScan = 1,
        projectile = 2
    }

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
        waitForReloadTime = new WaitForSeconds(weaponData.reloadTime);
        currentMethod = (Method)weaponData.method;
        currentBulletsCount = weaponData.bulletCount;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (isReadyToFire)
        {
            if (currentBulletsCount > 0)
            {
                StartCoroutine(FireCoroutine());
            }
            else
            {
                StartCoroutine(ReloadCoroutine());
            }
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
        currentBulletsCount--;

        Debug.Log(currentBulletsCount);

        yield return waitForFireDelay; 

        isReadyToFire = true; 
    }

    private IEnumerator ReloadCoroutine()
    {
        isReadyToFire = false;

        Debug.Log("isReloading");

        Vector3 originalPosition = transform.localPosition;
        Vector3 loweredPosition = originalPosition + new Vector3(0.1f, -0.5f, 0);

        float elapsedTime = 0f;
        float duration = weaponData.reloadTime / 3; 
        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, loweredPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = loweredPosition; 

        elapsedTime = 0f;

        yield return new WaitForSeconds(duration);

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(loweredPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;

        //yield return waitForReloadTime;

        currentBulletsCount = weaponData.bulletCount;

        isReadyToFire = true;
    }


    #region bulletPool
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
    #endregion
}
