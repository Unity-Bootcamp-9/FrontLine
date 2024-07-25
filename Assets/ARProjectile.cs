using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARProjectile : MonoBehaviour
{
    public float bulletSpeed = 1f;
    private Vector3 shootDirection;
    void OnEnable()
    {
        shootDirection = Camera.main.transform.forward;
    }

    void Update()
    {
        transform.position += shootDirection * Time.deltaTime * bulletSpeed;
    }
}
