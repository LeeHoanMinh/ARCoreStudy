using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{

    Camera currentCamera;

    void Start()
    {
        currentCamera = ModeManager.instance.GetMainCamera();    
    }

    void Update()
    {
        if (!ModeManager.instance.InEditorMode())
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //Get touch from user
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //Tap on UI element 
                }
                else
                {
                    //Tap on screen element
                    Ray ray = currentCamera.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hitObject;

                    if (Physics.Raycast(ray, out hitObject))
                    {
                        GameObject gameObject = hitObject.transform.gameObject;
                        if ((gameObject != null) && (gameObject.tag == "Enemy"))
                            gameObject.GetComponent<Enemy>().BeShot();

                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
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
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Canvas2DManager.instance.PutOriginalPlane();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Canvas2DManager.instance.FinishPutPlane();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Canvas2DManager.instance.PlaceObject();
            }
        }
    }
}
