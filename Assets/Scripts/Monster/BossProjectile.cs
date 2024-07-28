using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player"))
        {

            Destroy(gameObject);
        }
    }
}
