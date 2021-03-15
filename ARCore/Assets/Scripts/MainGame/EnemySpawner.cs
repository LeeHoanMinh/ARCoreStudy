using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    int lv;
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
            for (int j = 0;j < thisRound.numberOfGroups;j++)
            {
                List<int> permutationArray = new List<int>(spawnPlace.Length);
                RandomArray(ref permutationArray, spawnPlace.Length);
                int cnt = 0;
                foreach (GameObject enemyX in thisRound.enemyInstance)
                {
                    GameObject newG = Instantiate(enemyX);
                    newG.transform.position = spawnPlace[permutationArray[cnt]].position;
                    ++cnt;
                }
                yield return new WaitForSeconds(thisRound.waitBetweenEachGroup);
            }
            
        }
        SystemManager.instance.LevelComplete(lv);
        Destroy(this.gameObject);
        
    }

    void RandomArray(ref List<int> array,int length)
    {
        for (int i = 0;i < length;i++)
        {
            array.Add(i);
        }
        for (int i = 0;i < 10 * array.Count;i++)
        {
            int x = Random.Range(0, array.Count);
            int y = Random.Range(0, array.Count);
            int tmp = array[x];
            array[x] = array[y];
            array[y] = tmp;
        }
    }
}
