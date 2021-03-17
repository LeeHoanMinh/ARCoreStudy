using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager instance;

    public GameObject defaultAnchor;
    public GameObject enemyHeathBar;
    public GameObject ground;
    public GameObject airplane;
    public GameObject building;
    
    public GameObject minusHealth;
    public GameObject ScoreText;
    public GameObject enemyHealthBarPrefab;
    public GameObject[] enemySpawner;

    public GameObject playerProjectile;
    public GameObject enemyProjectile;
    public GameObject explosion;
    private void Awake()
    {
        //Instantiate(objectToSpawn[1]);
        if (instance == null)
            instance = this;
    }
}
