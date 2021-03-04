using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    Vector3 relativeDistance;
    private void Awake()
    {
        relativeDistance = objectToFollow.transform.position - this.transform.position;
    }
    private void Update()
    {
        this.transform.position = objectToFollow.transform.position - relativeDistance;
    }
}
