using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmoveObjectAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 originPosition;
    void Start()
    {
        originPosition = this.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position += originPosition;
    }
}
