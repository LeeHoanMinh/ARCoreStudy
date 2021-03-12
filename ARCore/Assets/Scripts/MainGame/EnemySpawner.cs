using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPlace;
    public EnemySpawnerRound[] enemySpawnerRound;
    public void StartSpawning()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        for (int i = 0;i < enemySpawnerRound.Length;i++)
        {
            EnemySpawnerRound thisRound = enemySpawnerRound[i];
            yield return new WaitForSeconds(thisRound.waitForTheFirstSpawn);
            int enemyCnt = 0;
            int enemyLV = 0;
            for (int j = 0;j < thisRound.enemytypes.numberOfEnemy;j++)
            {
                GameObject newG = Instantiate(thisRound.enemytypes.enemyInstance);
                //newG.GetComponent<Enemy>().EnemySetUp(5 + enemyLV);
                int position = Random.Range(0, 5);
                newG.transform.position = spawnPlace[position].position;
                yield return new WaitForSeconds(4f);
                enemyCnt++;
                if (enemyCnt == 10)
                {
                    yield return new WaitForSeconds(thisRound.waitBetweenEachGroup);
                    enemyCnt = 0;
                    enemyLV++;
                }

            }
        }
        
    }
}
