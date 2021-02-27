using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlane : MonoBehaviour
{
    float planeRadius;
    void Start()
    {
        planeRadius = this.transform.localScale.x;    
    }
    public bool CheckObjectInPlaneRadius(Vector3 objectPosition)
    {
        float xDistance, zDistance, xzDistance;
        xDistance = objectPosition.x - this.transform.position.x;
        zDistance = objectPosition.z - this.transform.position.z;
        xzDistance = Mathf.Sqrt(xDistance * xDistance + zDistance * zDistance);
        return (xzDistance <= planeRadius/2); 
    }
}
