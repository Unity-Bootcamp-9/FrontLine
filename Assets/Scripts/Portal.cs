using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private int monsterIndex;
    private WaitForSeconds waitForSpawnDelay;

    public void Initialize(int monsterIndex, float spawnDelay)
    {
        this.monsterIndex = monsterIndex;
        waitForSpawnDelay = new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        yield return waitForSpawnDelay;
        
    }
}