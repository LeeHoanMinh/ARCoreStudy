using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public GameObject plane;
    public GameObject placement;
    public GameObject title;
    public GameObject UIcanvas;
    void Start()
    {
        GameObject newObject;
        newObject = Instantiate(plane);
        newObject.transform.position = placement.transform.position;
        newObject.transform.position += new Vector3(0f, 0.02f, 0f);
        GameObject newTitle;
        newTitle = Instantiate(title);
        newTitle.transform.SetParent(UIcanvas.transform);
        newTitle.transform.position = newObject.transform.position + new Vector3(0f, 0.05f, 0f);
        newTitle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
}
