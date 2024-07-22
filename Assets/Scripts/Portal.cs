using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void Initialize(List<int> monsterIndexs, List<float> spawnDelays)
    {
        for(int i = 0; i < monsterIndexs.Count; i++)
        {
            StartCoroutine(SpawnMonster(monsterIndexs[i], spawnDelays[i]));
        }
    }
    private IEnumerator SpawnMonster(int index, float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Managers.SpawnManager.Spawn(transform.position, transform.rotation, index);
        }
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

}