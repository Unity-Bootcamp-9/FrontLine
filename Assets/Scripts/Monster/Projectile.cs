using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] public int damage { get; private set; }
    [SerializeField] private Vector3 targetPos;
    private string path;
    private Tween moveTween;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetTarget(Vector3 _startPos, Vector3 target, float duration, string path = null)
    {
        this.path = path;
        gameObject.transform.position = _startPos;
        targetPos = target;
        moveTween = transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HitTarget();
            GameManager.Instance.GetDamage(damage);
        }
    }

    public void HitTarget()
    {
        moveTween.Kill();

        if (path != null)
        {
            Monster.ProjectileReturnToPool(path, this);
        }
        else
        {
            GameObject effect = Managers.Resource.Instantiate("MonsterProjectile/FireballExplosion");
            effect.transform.SetParent(Managers.Instance.game.transform);
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        moveTween.Kill();
    }
}
