using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject cube;
    public GameObject parent;
    void Start()
    {
        GameObject newGame = Instantiate(cube,parent.transform);
        //newGame.transform.rotation = parent.transform.rotation * newGame.transform.rotation;
        newGame.transform.localPosition = new Vector3(0f, 4f, 0f);
    }

}
