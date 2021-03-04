using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILookAt : MonoBehaviour
{
    Camera currentCamera;
    void Start()
    {
        currentCamera = ModeManager.instance.GetMainCamera();
    }
    private void Update()
    {
        this.transform.LookAt(2 * this.transform.position - currentCamera.transform.position);
    }
}
