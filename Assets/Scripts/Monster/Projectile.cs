using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Vector3 targetPos;
    private Vector3 startPos;
    public float timeCount;

    private void OnEnable()
    {
        timeCount = 0;
    }

    public void SetTarget(Vector3 _startPos, Vector3 target)
    {
        startPos = _startPos;
        targetPos = target;
        transform.DOMove(targetPos, 3f).SetEase(Ease.OutQuad);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(startPos, targetPos, timeCount * speed);
        timeCount += Time.deltaTime;
    }

}
