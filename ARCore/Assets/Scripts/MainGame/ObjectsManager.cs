using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager instance;
    public GameObject defaultAnchor;
    public GameObject enemyHeathBar;
    public GameObject[] objectToSpawn;

    private void Awake()
    {
        //Instantiate(objectToSpawn[1]);
        if (instance == null)
            instance = this;
    }
}
