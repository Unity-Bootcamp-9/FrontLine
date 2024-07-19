using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1f;
    private Vector3 shootDirection;

    void OnEnable()
    {
        shootDirection = Camera.main.transform.forward;
        transform.rotation = Quaternion.LookRotation(shootDirection);
    }

    void Update()
    {
        transform.position += shootDirection * Time.deltaTime * bulletSpeed;
    }
}
