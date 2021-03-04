using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    Camera arCamera;
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Get touch from user
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                //Tap on UI element 
            }
            else
            {
                //Tap on screen element
                Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hitObject;

                if(Physics.Raycast(ray, out hitObject))
                {
                    GameObject gameObject = hitObject.transform.gameObject;
                    if ((gameObject != null) && (gameObject.tag == "Enemy"))
                        gameObject.GetComponent<Enemy>().BeShot();

                }
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
               
                GameObject gameObject = hitObject.transform.gameObject;
                if ((gameObject != null) && (gameObject.tag == "Enemy"))
                {
                    Debug.Log("shot");
                    gameObject.GetComponent<Enemy>().BeShot();
                }
            }
        }
    }
}
