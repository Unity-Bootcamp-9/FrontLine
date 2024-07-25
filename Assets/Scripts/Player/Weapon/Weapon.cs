using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngineInternal;
using static GameManager;

public class Weapon : MonoBehaviour
{
    [Header("DataField")]
    [SerializeField] private WeaponData weaponData;

    [SerializeField] private WaitForSeconds waitForFireDelay;
    [SerializeField] private WaitForSeconds delayAfterReload;
    [SerializeField] private Method currentMethod;
    [SerializeField] private int currentBulletsCount;
    [SerializeField] private Transform gunMuzzle;

    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isReadyToFire = true;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ObjectPool<GameObject> bulletPool;

    public delegate void BulletChanged(int currentBulletsCount);
    public event BulletChanged OnBulletChanged;

    public enum Method
    {
        hitScan = 1,
        projectile = 2
    }

    private void Awake()
    {
        player = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    public void Initialize(WeaponData weapon)
    {
        weaponData = weapon;
        bulletPrefab = Resources.Load<GameObject>(weaponData.bulletPrefab);

        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab�� �ε��� �� �����ϴ�: " + weaponData.bulletPrefab);
            return;
        }

        bulletPool = new ObjectPool<GameObject>(
            createFunc: CreateBullet,
            actionOnGet: ActivateBullet,
            actionOnRelease: DeactivateBullet,
            actionOnDestroy: DestroyBullet,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );

        waitForFireDelay = new WaitForSeconds(weaponData.fireDelay);
        delayAfterReload = new WaitForSeconds(0.3f);
        currentMethod = (Method)weaponData.method;
        currentBulletsCount = weaponData.bulletCount;
    }

    private void Update()
    {
        Debug.Log(currentBulletsCount);
    }

    public void Fire()
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

    public float CheckBulletLeft()
    {
        float bulletValue = currentBulletsCount;
        return bulletValue;
    }

    public void ReloadButton()
    {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        isReadyToFire = false;

        animator.Play("Shot", 0, 0);

        GameObject bullet = bulletPool.Get();

        if (currentMethod == Method.hitScan)
        {
            RaycastHit hit;

            if (Physics.Raycast(player.position, player.forward, out hit, weaponData.range))
            {
                if(hit.transform.TryGetComponent<Monster>(out Monster hitTarget))
                {
                    hitTarget.GetDamage(weaponData.attackDamage);
                    
                }
            }
        }

        Handheld.Vibrate();

        currentBulletsCount--;
        OnBulletChanged?.Invoke(currentBulletsCount);


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
        float duration = weaponData.reloadTime / 3; // �����ð��� 3���� ������ ���� �ִϸ��̼� ����
        while (elapsedTime < duration) // �� ������ ����
        {
            transform.localPosition = Vector3.Lerp(originalPosition, loweredPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = loweredPosition;

        elapsedTime = 0f;

        yield return new WaitForSeconds(duration); //������ ��ٸ���

        while (elapsedTime < duration) // �ø��� ����
        {
            transform.localPosition = Vector3.Lerp(loweredPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;

        yield return delayAfterReload; // �ø��� 0.3�� ���

        currentBulletsCount = weaponData.bulletCount;
        OnBulletChanged?.Invoke(currentBulletsCount);

        isReadyToFire = true;
    }


    #region bulletPool
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<WeaponProjectile>().Initialize(bulletPool, weaponData.bulletSpeed, weaponData.attackDamage, currentMethod);
        return bullet;
    }

    private void ActivateBullet(GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.transform.position = gunMuzzle.position; // �Ѿ� Get�� �� ��ġ ���� 
        bullet.transform.rotation = Quaternion.LookRotation(gunMuzzle.forward); // �Ѿ� Get�� �� ���� ����
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
