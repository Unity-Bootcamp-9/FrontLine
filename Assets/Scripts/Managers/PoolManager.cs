using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager :MonoBehaviour
{
    private bool collectionChecks = true;
    private int maxPoolSize = 10;

    public Dictionary<string, IObjectPool<MonoBehaviour>> pools = new Dictionary<string, IObjectPool<MonoBehaviour>>();

    public void MakeObjectPool<T>(T prefab) where T : MonoBehaviour
    {
        if (pools.ContainsKey(prefab.name))
        {
            Debug.LogWarning($"Pool for {prefab.name} already exists.");
            return;
        }

        IObjectPool<MonoBehaviour> pool = new ObjectPool<MonoBehaviour>(
            () => CreateObject(prefab),
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionChecks,
            10,
            maxPoolSize
        );

        pools.Add(prefab.name, pool);
    }

    private T CreateObject<T>(T prefab) where T : MonoBehaviour
    {
        return Instantiate(prefab);
    }

    private void OnTakeFromPool(MonoBehaviour obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(MonoBehaviour obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(MonoBehaviour obj)
    {
        Destroy(obj.gameObject);
    }
}
