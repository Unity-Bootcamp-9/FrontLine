using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header ("DataField")]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] WaitForSeconds waitForFireDelay;

    [Header("TestField")]
    public GameObject bulletPrefabTest;

    [SerializeField] Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] Transform gunMuzzle;
    [SerializeField] Transform rayCastTarget;
    [SerializeField] private bool isShootReady = false;
    [SerializeField] private bool isHitScan = false;

    private void Awake()
    {
        player = Camera.main.transform;
        animator = GetComponent<Animator>();
    }


    public void Initialize(WeaponData weapon)
    {
        weaponData = weapon;
        bulletPrefabTest = Resources.Load<GameObject>(weaponData.bulletPrefab);
        waitForFireDelay = new WaitForSeconds(weaponData.fireDelay);
        isShootReady = true;
        if(weaponData.method == 1)
        {
            isHitScan = true;
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && isShootReady) 
        {
            Fire();
        }
    }

    public void Fire()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        isShootReady = false;

        animator.Play("Shot", 0, 0);

        Instantiate(bulletPrefabTest, gunMuzzle.position, Quaternion.identity);

        if(isHitScan) 
        {
            RaycastHit hit;

            if (Physics.Raycast(player.position, player.forward, out hit, 20f))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }

        Handheld.Vibrate();

        yield return waitForFireDelay;

        isShootReady = true;
    }

    

    public void Reload()
    {

    }
}
