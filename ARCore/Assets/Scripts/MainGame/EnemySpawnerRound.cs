using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemySpawnerRound
{

    public float waitForTheFirstSpawn = 0;
    public float numberOfGroups = 0;
    public float waitBetweenEachGroup = 0;

    public GameObject[] enemyInstance;

}
