using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EffectManager
{
    private Dictionary<string, IObjectPool<Effect>> effects = new Dictionary<string, IObjectPool<Effect>>();

    public Effect GetEffect(string path, Vector3 position, Quaternion rotation, float duration = 1f)
    {
        Effect effect = null;
        if (effects.ContainsKey(path))
        {
            effect = effects[path].Get();
            effect.transform.position = position;
            effect.transform.rotation = rotation;
            effect.Initialize(path, duration);
            return effect;
        }

        effect = Managers.Resource.Load<Effect>(path);
        MakeObjectPool(effect, path);
        effect = effects[path].Get();
        effect.transform.position = position;
        effect.transform.rotation = rotation;
        effect.Initialize(path, duration);
        return effect;
    }

    public void EffectReturnToPool(string path, Effect effect)
    {
        effects[path].Release(effect);
    }

    private bool collectionChecks = true;
    private int maxPoolSize = 10;

    private void MakeObjectPool(Effect prefab, string path)
    {
        if (effects.ContainsKey(prefab.name))
        {
            Debug.LogWarning($"Pool for {prefab.name} already exists.");
            return;
        }

        IObjectPool<Effect> pool = new ObjectPool<Effect>(
            () => CreateObject(prefab),
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionChecks,
            10,
            maxPoolSize
        );

        effects.Add(path, pool);
    }

    private Effect CreateObject(Effect prefab)
    {
        return Object.Instantiate(prefab);
    }

    private void OnTakeFromPool(Effect obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Effect obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Effect obj)
    {
        Object.Destroy(obj.gameObject);
    }
}
