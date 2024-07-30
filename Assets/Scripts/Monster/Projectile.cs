using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public int damage { get; private set; }
    [SerializeField] private Vector3 targetPos;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetTarget(Vector3 _startPos, Vector3 target, float duration)
    {
        gameObject.transform.position = _startPos;
        targetPos = target;
        transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GetDamage(damage);
            Destroy(gameObject);
        }
    }

}
