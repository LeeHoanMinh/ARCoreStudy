using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    Camera currentCamera;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
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

                }
            }
        }
        else
        {
 
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
