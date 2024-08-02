using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngineInternal;

public class Weapon : MonoBehaviour
{
    [Header("DataField")]
    [SerializeField] private WeaponData weaponData;

    [SerializeField] private WaitForSeconds waitForFireDelay;
    [SerializeField] private WaitForSeconds delayAfterReload;
    [SerializeField] private Method currentMethod;
    [SerializeField] private int attackDamage;
    [SerializeField] private int currentBulletsCount;
    [SerializeField] private Transform gunMuzzle;
    [SerializeField] private Transform autoLazerTransform;
    [SerializeField] private int range = 20;

    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isReadyToFire = true;
    [SerializeField] private bool isReloading = false;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ObjectPool<GameObject> bulletPool;

    [SerializeField] private LayerMask monsterLayer;
    
    private ObjectPool<LineRenderer> lineRendererPool;
    [SerializeField] private Transform lineRendererParent;

    private AudioSource weaponAudioSource;

    public delegate void BulletChanged(int currentBulletsCount);
    public event BulletChanged OnBulletChanged;


    public LayerMask Enemy;
    public Collider[] hitEnemies;
    private int maxEnemies = 10;
    private AudioClip audioClip;

    private bool Vibrate;

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
        weaponAudioSource = gameObject.AddComponent<AudioSource>();
        isReloading = false;
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

        currentMethod = (Method)weaponData.method;
        switch (currentMethod)
        {
            case Method.AutoLazer:
                 // Initialize LineRenderer Pool
                lineRendererPool = new ObjectPool<LineRenderer>(
                    createFunc: CreateLineRenderer,
                    actionOnGet: ActivateLineRenderer,
                    actionOnRelease: DeactivateLineRenderer,
                    actionOnDestroy: DestroyLineRenderer,
                    collectionCheck: false,
                    defaultCapacity: maxEnemies,
                    maxSize: maxEnemies);
                break;
            case Method.hitScan:
            case Method.projectile:
                // Initialize Bullet Pool
                bulletPool = new ObjectPool<GameObject>(
                    createFunc: CreateBullet,
                    actionOnGet: ActivateBullet,
                    actionOnRelease: DeactivateBullet,
                    actionOnDestroy: DestroyBullet,
                    collectionCheck: false,
                    defaultCapacity: 10,
                    maxSize: 20);
                break;
        }


        waitForFireDelay = new WaitForSeconds(weaponData.fireDelay);
        delayAfterReload = new WaitForSeconds(0.3f);
        currentBulletsCount = weaponData.bulletCount;
        attackDamage = weaponData.attackDamage;
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
        if(!isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator FireCoroutine()
    {
        isReadyToFire = false;

        animator.Play("Shot", 0, 0);

        switch (currentMethod)
        {
            case Method.hitScan:
                bulletPool.Get();
                RaycastHit hit;
                
                if(Physics.Raycast(player.position, player.forward, out hit, weaponData.range, monsterLayer))
                {
                    if (hit.transform.CompareTag("Monster"))
                    {
                        hit.transform.GetComponentInParent<IMonster>().GetDamage(attackDamage);
                    }
                    else if(hit.transform.CompareTag("MonsterProjectile"))
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
                break;

            case Method.projectile:
                bulletPool.Get();
                //투사체 
                break;

            case Method.AutoLazer:
                Vector3 boxCenter = autoLazerTransform.position;
                Vector3 boxHalfExtents = new Vector3(13, 2, 2);
                Quaternion boxOrientation = gunMuzzle.rotation;

                int enemyCount = Physics.OverlapBoxNonAlloc(boxCenter, boxHalfExtents, hitEnemies, boxOrientation, monsterLayer);

                // 적에게 전기 효과와 피해를 주기
                for (int i = 0; i < enemyCount; i++)
                {
                    Collider collider = hitEnemies[i];
                    if(collider.CompareTag("Monster"))
                    {
                    IMonster monster = collider.GetComponentInParent<IMonster>();
                    ShowElectricEffect(gunMuzzle.position, collider.transform.position);
                    monster.GetDamage(weaponData.attackDamage);
                    }
                    else
                    {
                        Destroy(collider.gameObject);
                    }
                }
                break;
        }
        
        if (!GameManager.muteVibration)
        {
            Handheld.Vibrate();
        }

        currentBulletsCount--;
        OnBulletChanged?.Invoke(currentBulletsCount);
        weaponAudioSource.PlayOneShot(Managers.SoundManager.GetAudioClip(weaponData.soundFX));
        yield return waitForFireDelay;

        isReadyToFire = true;
    }

    private void ShowElectricEffect(Vector3 start, Vector3 end)
    {
        LineRenderer lineRenderer = lineRendererPool.Get();
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        StartCoroutine(AutoOffElectricEffect(lineRenderer, 0.05f));
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
        isReloading = true;
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
        isReloading = false;
    }

    #region bulletPool
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<WeaponProjectile>().Initialize(bulletPool, weaponData.bulletSpeed, weaponData.attackDamage, currentMethod, weaponData.bulletVisualFX);
        bullet.transform.parent = Managers.Instance.game.transform;
        return bullet;
    }

    private void ActivateBullet(GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.transform.position = gunMuzzle.position; // 총알 Get할 때 위치 설정 
        bullet.transform.rotation = Quaternion.LookRotation(gunMuzzle.forward); // 총알 Get할 때 방향 설정
        bullet.GetComponent<WeaponProjectile>().ShootProjectile();
    }

    private void DeactivateBullet(GameObject bullet)
    {
        bullet.transform.position = gunMuzzle.position;
        bullet.transform.rotation = Quaternion.LookRotation(gunMuzzle.forward); // 총알 Get할 때 방향 설정
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

        lineRenderer.startWidth = 0.08f;
        lineRenderer.endWidth = 0.08f;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.blue;
        lineRenderer.positionCount = 2;

        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineMaterial.color = Color.blue;
        lineRenderer.material = lineMaterial;

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
