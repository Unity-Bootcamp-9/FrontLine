//using System.Collections;
//using System.Collections.Generic;
//using Unity.XR.CoreUtils.Collections;
//using UnityEngine;
//using UnityEngine.Pool;

//public class SpawnManager
//{
//    private readonly bool collectionChecks = true;
//    private readonly int maxPoolSize = 10;

//    private Dictionary<int, Transform> poolContainers;
//    private Dictionary<int, IObjectPool<Monster>> monsterPools;

//    public void Initialize()
//    {
//        monsterPools = new Dictionary<int, IObjectPool<Monster>>();
//        poolContainers = new Dictionary<int, Transform>();
//    }

//    public void MakeObjectPool(Monster prefab)
//    {
//        IObjectPool<Monster> pool = new ObjectPool<Monster>(
//            () => CreateObject(prefab),
//            OnTakeFromPool,
//            OnReturnedToPool,
//            OnDestroyPoolObject,
//            collectionChecks,
//            10,
//            maxPoolSize
//        );
//        monsterPools.Add(prefab.monsterData.index, pool);
//    }

//    private Monster CreateObject(Monster prefab)
//    {
//        Monster newMonster = Object.Instantiate(prefab);
//        if (!poolContainers.ContainsKey(newMonster.monsterData.index))
//        {
//            poolContainers.Add(newMonster.monsterData.index, new GameObject($"Monster[{newMonster.monsterData.index}] Pool").transform);
//        }
//        newMonster.transform.SetParent(poolContainers[newMonster.monsterData.index]);
//        return newMonster;
//    }

//    private void OnTakeFromPool(Monster obj)
//    {
//        obj.gameObject.SetActive(true);
//    }

//    private void OnReturnedToPool(Monster obj)
//    {
//        obj.gameObject.SetActive(false);
//    }

//    private void OnDestroyPoolObject(Monster obj)
//    {
//        Object.Destroy(obj.gameObject);
//    }

//    public Monster Spawn(Vector3 position, Quaternion rotation, int index)
//    {
//        Monster monster = monsterPools[index].Get();
//        monster.transform.position = position;
//        monster.transform.rotation = rotation;
//        return monster;
//    }

//    public void ReturnToPool(Monster element, int index)
//    {
//        monsterPools[index].Release(element);
//    }

//    public void ResetPools()
//    {
//        monsterPools.Clear();
//        poolContainers.Clear();
//    }
//}