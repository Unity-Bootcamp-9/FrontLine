using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;



public class Weapon : MonoBehaviour
{
    [Header ("DataField")]
    private WeaponData weaponData;
    private WaitForSeconds waitForFireDelay;
    private ObjectPool<GameObject> bullets;
    private Method currentMethod;
    private enum Method
    {
        hitScan = 1, 
        projectile = 2
    }

    [Header("TestField")]
    public GameObject bulletPrefabTest;

     [SerializeField] private Transform player;
     [SerializeField] private Animator animator;
     [SerializeField] private Transform gunMuzzle;
     [SerializeField] private bool isShootReady = false;

    private void Awake()
    {
        player = Camera.main.transform;
        animator = GetComponent<Animator>();
        bullets = new ObjectPool<GameObject>(() => new GameObject());
    }


    public void Initialize(WeaponData weapon)
    {
        weaponData = weapon;
        bulletPrefabTest = Resources.Load<GameObject>(weaponData.bulletPrefab);
        waitForFireDelay = new WaitForSeconds(weaponData.fireDelay);
        SetFireMethod(weaponData.method);
        isShootReady = true;
    }

    private void SetFireMethod(int methodValue)
    {
        if(Enum.IsDefined(typeof(Method), methodValue))
        {
            currentMethod = (Method)methodValue;
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Fire();
            Debug.Log("Fire");
        }
    }

    private void Fire()
    {
        if (isShootReady)
        {
            StartCoroutine(FireCoroutine());
        }
    }

    private IEnumerator FireCoroutine()
    {
        isShootReady = false;

        animator.Play("Shot", 0, 0);

        Instantiate(bulletPrefabTest, gunMuzzle.position, Quaternion.identity);

        if(currentMethod == Method.hitScan) 
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
