using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARWeapon : MonoBehaviour, IARWeapon
{
    [SerializeField] public int bulletCount { get => 20; }
    [SerializeField] private Animator animator;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunMuzzle;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Fire();
        }
    }

    public void Fire()
    {
        animator.Play("Shot", 0, 0);
        Instantiate(bulletPrefab, gunMuzzle.position, Quaternion.identity);
    }

    public void Reload()
    {

    }
}
