using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Blaster : Weapon
{
    [SerializeField] Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunMuzzle;
    [SerializeField] Transform rayCastTarget;
    [SerializeField] private bool shootReady = true;
    [SerializeField] WaitForSeconds fireDelays;


    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        player = Camera.main.transform;
        animator = GetComponent<Animator>();
        shootReady = true;
        fireDelay = 0.5f;
        fireDelays = new WaitForSeconds(fireDelay);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && shootReady) 
        {
            Fire();
        }
    }

    public override void Fire()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        shootReady = false;

        animator.Play("Shot", 0, 0);

        Instantiate(bulletPrefab, gunMuzzle.position, Quaternion.identity);

        RaycastHit hit;

        if (Physics.Raycast(player.position, player.forward, out hit, 20f))
        {
            Debug.Log(hit.transform.gameObject.name);
        }

        Handheld.Vibrate();

        yield return fireDelays;

        shootReady = true;
    }

    public override void Reload()
    {

    }
}
