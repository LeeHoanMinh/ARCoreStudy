using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class Canvas2DManager : MonoBehaviour
{
    ARPlaneManager arPlaneManager;

    [SerializeField]
    Button toggleARPlaneButton;

    [SerializeField]
    Button putOriginalPlaneButton;

    [SerializeField]
    Button placeObjectButton;

    [SerializeField]
    Button resetSceneButton;

    [SerializeField]
    Button finishPutPlaneButton;
    private void Start()
    {
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        if(toggleARPlaneButton != null)
        {
            toggleARPlaneButton.onClick.AddListener(ToggleARPlane);
        }

        if(putOriginalPlaneButton != null)
        {
            putOriginalPlaneButton.onClick.AddListener(PutOriginalPlane);
        }

        if (placeObjectButton != null)
        {
            placeObjectButton.onClick.AddListener(PlaceObject);
        }

        if(resetSceneButton != null)
        {
            resetSceneButton.onClick.AddListener(ResetScene);
        }

        if(finishPutPlaneButton != null)
        {
            finishPutPlaneButton.onClick.AddListener(FinishPutPlane);
        }
    }

    void ResetScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    void ToggleARPlane()
    {
        arPlaneManager.enabled = !arPlaneManager.enabled;
        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(arPlaneManager.enabled);
        }
        if (arPlaneManager.enabled)
        {
            toggleARPlaneButton.GetComponentInChildren<Text>().text = "Disable AR Plane Detection";
        }
        else
        {
            toggleARPlaneButton.GetComponentInChildren<Text>().text = "Enable AR Plane Detection";
        }
    }

    void PutOriginalPlane()
    {   
        SystemManager.instance.ActivatePlaneInitialization();
        finishPutPlaneButton.gameObject.SetActive(true);
        putOriginalPlaneButton.gameObject.SetActive(false);
    }

    void PlaceObject()
    {
        if (SystemManager.instance.SystemState == 1)
        {
            SpawningManager.instance.SpawnPlane(SystemManager.instance.currentObjectToSpawn);
        }
        else
        {
            Vector3 objectPosition;
            objectPosition =  IndicatorManager.instance.PlacementIndicator.transform.position - SpawningManager.instance.OriginalTranslate.position;
            //objectPosition += new Vector3(0f, 0.05f,0f);
            SpawningManager.instance.SpawnObjectByPosition(SystemManager.instance.currentObjectToSpawn, objectPosition);
        }
    }

    void FinishPutPlane()
    {
        SystemManager.instance.FinishPlaneInitialization();
        finishPutPlaneButton.gameObject.SetActive(false);
    }
}
