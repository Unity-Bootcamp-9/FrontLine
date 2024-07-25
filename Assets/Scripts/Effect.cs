using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private string path;
    private float duration;

    public void Initialize(string path, float duration)
    {
        this.path = path;
        this.duration = duration;
        StartCoroutine(ReturnToPoolAfterDuration());
    }

    private IEnumerator ReturnToPoolAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Managers.EffectManager.EffectReturnToPool(path, this);
    }
}
