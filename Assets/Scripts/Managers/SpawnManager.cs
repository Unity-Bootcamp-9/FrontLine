using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager
{
    private readonly bool collectionChecks = true;
    private readonly int maxPoolSize = 10;
    private readonly int maxMonster = 4;

    IObjectPool<Monster>[] monsterPools;

    public void Initialize()
    {
        monsterPools = new IObjectPool<Monster>[maxMonster];
    }

    public void MakeObjectPool(Monster prefab)
    {
        IObjectPool<Monster> pool = new ObjectPool<Monster>(
            () => CreateObject(prefab),
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionChecks,
            10,
            maxPoolSize
        );
        monsterPools[prefab.monsterData.index] = pool;
    }

    private Monster CreateObject(Monster prefab)
    {
        return Object.Instantiate(prefab);
    }

    private void OnTakeFromPool(Monster obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Monster obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Monster obj)
    {
        Object.Destroy(obj.gameObject);
    }

    public void Spawn(Transform spawnTransform, int index)
    {
        monsterPools[index].Get().transform.position = spawnTransform.position;
        monsterPools[index].Get().transform.rotation = spawnTransform.rotation;
    }
}
