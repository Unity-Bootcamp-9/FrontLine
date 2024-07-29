using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Portal : MonoBehaviour
{
    private readonly bool collectionChecks = true;
    private readonly int maxPoolSize = 10;

    private static Dictionary<int, Transform> poolContainers = new Dictionary<int, Transform>();
    private static Dictionary<int, IObjectPool<Monster>> monsterPools = new Dictionary<int, IObjectPool<Monster>>();

    public void MakeObjectPool(Monster prefab)
    {
        if (!monsterPools.ContainsKey(prefab.monsterData.index))
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
            monsterPools.Add(prefab.monsterData.index, pool);
        }
    }

    private Monster CreateObject(Monster prefab)
    {
        Monster newMonster = Instantiate(prefab);
        if (!poolContainers.ContainsKey(newMonster.monsterData.index))
        {
            GameObject newPool = new GameObject($"Monster[{newMonster.monsterData.index}] Pool");
            poolContainers.Add(newMonster.monsterData.index, newPool.transform);
            newPool.transform.parent = Managers.Instance.game.transform;
        }
        newMonster.transform.SetParent(poolContainers[newMonster.monsterData.index]);
        return newMonster;
    }

    private void OnTakeFromPool(Monster obj)
    {
        obj.gameObject.SetActive(true);
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void OnReturnedToPool(Monster obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Monster obj)
    {
        Destroy(obj.gameObject);
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

    public void ResetPools()
    {
        monsterPools.Clear();
        foreach(var pool in poolContainers.Values)
        {
            Destroy(pool.gameObject);
        }
        poolContainers.Clear();
        StopSpawn();
    }

    public void InitializePool(int[] monsterIndexs)
    {
        for (int i = 0; i < monsterIndexs.Length; i++)
        {
            Monster monster = Resources.Load<Monster>("Monster/" + Managers.DataManager.monsterDatas[monsterIndexs[i]].name);
            monster.monsterData = Managers.DataManager.monsterDatas[i];
            MakeObjectPool(monster);
        }
    }

    public void Initialize(int[] monsterIndexs, float[] spawnDelays)
    {
        InitializePool(monsterIndexs);
        for (int i = 0; i < monsterIndexs.Length; i++)
        {
            StartCoroutine(SpawnMonster(monsterIndexs[i], spawnDelays[i]));
        }
    }

    private IEnumerator SpawnMonster(int index, float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Spawn(transform.position, transform.rotation, index).portal = this;
        }
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

}