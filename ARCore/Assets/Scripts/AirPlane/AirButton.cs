using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirButton : MonoBehaviour
{
    private void Update()
    {
        this.transform.LookAt(2 * this.transform.position - Camera.main.transform.position);
    }
}
