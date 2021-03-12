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
            foreach (EnemySpawnerRound.EnemyType enemyType in thisRound.enemytypes)
            {

                for (int j = 0; j < enemyType.numberOfEnemy; j++)
                {
                    GameObject newG = Instantiate(enemyType.enemyInstance);
                    
                    int position = Random.Range(0, 5);
                    newG.transform.position = spawnPlace[position].position;
                    yield return new WaitForSeconds(thisRound.timeToSpawn);
  
                }
        
            
            }
            yield return new WaitForSeconds(thisRound.waitBetweenEachGroup);
        }
        Destroy(this.gameObject);
        
    }
}
