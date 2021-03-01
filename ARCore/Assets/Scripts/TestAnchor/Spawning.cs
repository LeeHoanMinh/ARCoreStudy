using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject cube;
    public GameObject parent;
    void Start()
    {
        Instantiate(cube, parent.transform);

    }

}
