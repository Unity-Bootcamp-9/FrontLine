using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage { get; private set; }
    private Vector3 targetPos;
    private Vector3 startPos;
    private float timeCount;
    private float lerpT;


    private void OnEnable()
    {
        timeCount = 0;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetTarget(Vector3 _startPos, Vector3 target)
    {
        startPos = _startPos;
        targetPos = target;
        transform.DOMove(targetPos, 3f).SetEase(Ease.OutQuad);
    }

    private void Update()
    {
        lerpT = timeCount * speed;
        transform.position = Vector3.Lerp(startPos, targetPos, lerpT);
        timeCount += Time.deltaTime;
        if (lerpT >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GetDamage(damage);
        }
    }

}
