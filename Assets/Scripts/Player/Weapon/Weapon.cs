using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("DataField")]
    [SerializeField] private WeaponData weaponData;

    [SerializeField] private WaitForSeconds waitForFireDelay;
    [SerializeField] private WaitForSeconds delayAfterReload;
    [SerializeField] private Method currentMethod;
    [SerializeField] private int currentBulletsCount;
    [SerializeField] private Transform gunMuzzle;
    [SerializeField] private Transform autoLazerTransform;
    [SerializeField] private int range = 20;

    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isReadyToFire = true;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ObjectPool<GameObject> bulletPool;

    private ObjectPool<LineRenderer> lineRendererPool;
    [SerializeField] private Transform lineRendererParent;

    public delegate void BulletChanged(int currentBulletsCount);
    public event BulletChanged OnBulletChanged;

    public LayerMask Enemy;
    public Collider[] hitEnemies;
    private int maxEnemies = 10;

    public enum Method
    {
        hitScan = 1,
        projectile = 2,
        AutoLazer = 3
    }

    private void Awake()
    {
        player = Camera.main.gameObject.transform;
        animator = GetComponent<Animator>();
    }

    public void Initialize(WeaponData weapon)
    {
        hitEnemies = new Collider[maxEnemies];
        weaponData = weapon;
        bulletPrefab = Resources.Load<GameObject>(weaponData.bulletPrefab);

        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab을 로드할 수 없습니다: " + weaponData.bulletPrefab);
            return;
        }

        // Initialize Bullet Pool
        bulletPool = new ObjectPool<GameObject>(
            createFunc: CreateBullet,
            actionOnGet: ActivateBullet,
            actionOnRelease: DeactivateBullet,
            actionOnDestroy: DestroyBullet,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );

        // Initialize LineRenderer Pool
        lineRendererPool = new ObjectPool<LineRenderer>(
            createFunc: CreateLineRenderer,
            actionOnGet: ActivateLineRenderer,
            actionOnRelease: DeactivateLineRenderer,
            actionOnDestroy: DestroyLineRenderer,
            collectionCheck: false,
            defaultCapacity: maxEnemies,
            maxSize: maxEnemies
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(autoLazerTransform.position, range);
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

    public int CheckBulletLeft()
    {
        float bulletValue = currentBulletsCount;
        int value = Mathf.RoundToInt(bulletValue);
        return value;
    }

    public void ReloadButton()
    {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        isReadyToFire = false;

        animator.Play("Shot", 0, 0);

        bulletPool.Get();

        switch (currentMethod)
        {
            case Method.hitScan:
                RaycastHit hit;
                Physics.Raycast(player.position, player.forward, out hit, weaponData.range);
                if (hit.transform.TryGetComponent<Monster>(out Monster hitTarget))
                {
                    hitTarget.GetDamage(weaponData.attackDamage);
                }
                break;

            case Method.projectile:
                // Projectile logic here
                break;

            case Method.AutoLazer:
                int enemyCount = Physics.OverlapSphereNonAlloc(autoLazerTransform.position, range, hitEnemies, Enemy);

                for (int i = 0; i < enemyCount; i++)
                {
                    Collider collider = hitEnemies[i];
                    Transform parentTransform = collider.transform.parent;
                    Monster monster = parentTransform.GetComponent<Monster>();
                    if (monster != null)
                    {
                        ShowElectricEffect(gunMuzzle.position, monster.transform.position);
                        monster.GetDamage(weaponData.attackDamage);
                    }
                }
                break;
        }

        Handheld.Vibrate();

        currentBulletsCount--;
        OnBulletChanged?.Invoke(currentBulletsCount);

        Debug.Log(currentBulletsCount);

        yield return waitForFireDelay;

        isReadyToFire = true;
    }

    private void ShowElectricEffect(Vector3 start, Vector3 end)
    {
        LineRenderer lineRenderer = lineRendererPool.Get();
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        StartCoroutine(AutoOffElectricEffect(lineRenderer, 0.2f));
    }

    private IEnumerator AutoOffElectricEffect(LineRenderer lineRenderer, float delay)
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(delay);
        lineRenderer.enabled = false;
        lineRendererPool.Release(lineRenderer);
    }

    private IEnumerator ReloadCoroutine()
    {
        isReadyToFire = false;

        Debug.Log("isReloading");

        Vector3 originalPosition = transform.localPosition;
        Vector3 loweredPosition = originalPosition + new Vector3(0.1f, -0.5f, 0);

        float elapsedTime = 0f;
        float duration = weaponData.reloadTime / 3; // 장전시간을 3으로 나눠서 장전 애니메이션 실행
        while (elapsedTime < duration) // 총 내리는 보간
        {
            transform.localPosition = Vector3.Lerp(originalPosition, loweredPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = loweredPosition;

        elapsedTime = 0f;

        yield return new WaitForSeconds(duration); //내리고 기다리기

        while (elapsedTime < duration) // 올리는 보간
        {
            transform.localPosition = Vector3.Lerp(loweredPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;

        yield return delayAfterReload; // 올리고 0.3초 대기

        currentBulletsCount = weaponData.bulletCount;
        OnBulletChanged?.Invoke(currentBulletsCount);

        isReadyToFire = true;
    }

    #region bulletPool
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<WeaponProjectile>().Initialize(bulletPool, weaponData.bulletSpeed, weaponData.attackDamage, currentMethod, weaponData.bulletVisualFX);
        return bullet;
    }

    private void ActivateBullet(GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.transform.position = gunMuzzle.position; // 총알 Get할 때 위치 설정 
        bullet.transform.rotation = Quaternion.LookRotation(gunMuzzle.forward); // 총알 Get할 때 방향 설정
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

    #region lineRendererPool
    private LineRenderer CreateLineRenderer()
    {
        GameObject lineObj = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        lineObj.transform.SetParent(lineRendererParent);

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;

        return lineRenderer;
    }

    private void ActivateLineRenderer(LineRenderer lineRenderer)
    {
        lineRenderer.enabled = true;
    }

    private void DeactivateLineRenderer(LineRenderer lineRenderer)
    {
        lineRenderer.enabled = false;
    }

    private void DestroyLineRenderer(LineRenderer lineRenderer)
    {
        Destroy(lineRenderer.gameObject);
    }
    #endregion
}
