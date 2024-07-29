using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private float elapsedTime = 0f;
    [SerializeField] private float moveSpeed = 3f;

    private void Update()
    {
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > delay)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
