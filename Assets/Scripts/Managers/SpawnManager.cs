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

    private IObjectPool<Monster>[] monsterPools;
    private Transform[] PoolContainers;

    public void Initialize()
    {
        monsterPools = new IObjectPool<Monster>[maxMonster];
        PoolContainers = new Transform[maxMonster];
        for (int i = 1; i < PoolContainers.Length; i++)
        {
            PoolContainers[i] = new GameObject($"MonsterPool[{i}]").transform;
        }
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
        Monster newMonster = Object.Instantiate(prefab);
        newMonster.transform.SetParent(PoolContainers[newMonster.monsterData.index]);
        return newMonster;
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

    public Monster Spawn(Vector3 position, Quaternion rotation, int index)
    {
        Monster monster = monsterPools[index].Get();
        monster.transform.position = position;
        monster.transform.rotation = rotation;
        return monster;
    }

    public void ReturnToPool(Monster element, int index)
    {
        monsterPools[index].Release(element);
    }
}
