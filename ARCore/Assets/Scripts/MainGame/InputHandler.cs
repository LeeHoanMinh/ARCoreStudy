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
        SystemManager.instance.center.transform.localScale = new Vector3(1f, 1f, 1f);
        if (!ModeManager.instance.InEditorMode())
        {
            Ray ray = currentCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit[] hitObject = new RaycastHit[10];
            int hitCnt = Physics.RaycastNonAlloc(ray, hitObject);



            for (int i = 0; i < hitCnt; i++)
            {
                if (hitObject[i].collider != null)
                {
                    //SimpleSound.instance.PlaySound();
                    GameObject gameObject = hitObject[i].transform.gameObject;
                    for (int t = 0; t < 6; t++)
                    {
                        if (gameObject.transform.parent != null)
                            gameObject = gameObject.transform.parent.gameObject;
                    }

                    if ((gameObject != null) && (gameObject.tag == "Enemy"))
                    {
                        SystemManager.instance.center.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        break;
                    }
                }
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //Get touch from user
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //Tap on UI element 

                }
                else
                {
                    Player.instance.Shoot();
                }
            }
        }
        else
        {
            Ray ray = currentCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit[] hitObject = new RaycastHit[10];
            int hitCnt = Physics.RaycastNonAlloc(ray, hitObject);


            
            for (int i = 0; i < hitCnt; i++)
            {
                if (hitObject[i].collider != null)
                {
                    SimpleSound.instance.PlaySound();
                    GameObject gameObject = hitObject[i].transform.gameObject;
                    for (int t = 0; t < 6; t++)
                    {
                        if (gameObject.transform.parent != null)
                            gameObject = gameObject.transform.parent.gameObject;
                    }

                    if ((gameObject != null) && (gameObject.tag == "Enemy"))
                    {
                        SystemManager.instance.center.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        break;
                    }
                }
            }






            if (Input.GetKeyDown(KeyCode.Q))
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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Canvas2DManager.instance.Shoot();
            }
        }
    }


}
