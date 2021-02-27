using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager instance;
    public GameObject[] objectToSpawn;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
