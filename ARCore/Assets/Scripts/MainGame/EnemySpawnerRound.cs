using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemySpawnerRound
{

    public float waitForTheFirstSpawn = 0;
    public float waitBetweenEachGroup = 0;
    
    [Serializable]
    public class EnemyType
    {
        public GameObject enemyInstance;
        public float numberOfEnemy;
    }
    public EnemyType[] enemytypes;
    public float timeToSpawn;

}
