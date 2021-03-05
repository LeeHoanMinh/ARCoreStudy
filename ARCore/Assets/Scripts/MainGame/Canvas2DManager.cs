using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class Canvas2DManager : MonoBehaviour
{
    public static Canvas2DManager instance;
    ARPlaneManager arPlaneManager;

    [SerializeField]
    Button toggleARPlaneButton;

    [SerializeField]
    Button putOriginalPlaneButton;

    [SerializeField]
    Button placeObjectButton;


    [SerializeField]
    Button finishPutPlaneButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
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

        if(finishPutPlaneButton != null)
        {
            finishPutPlaneButton.onClick.AddListener(FinishPutPlane);
        }
    }

 
    void ToggleARPlane()
    {
        SimpleSound.instance.PlayButton();
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

    public void PutOriginalPlane()
    {
        SimpleSound.instance.PlayButton();
        SystemManager.instance.ActivatePlaneInitialization();
        finishPutPlaneButton.gameObject.SetActive(true);
        putOriginalPlaneButton.gameObject.SetActive(false);
    }

    public void PlaceObject()
    {
        SimpleSound.instance.PlayButton();
        if (SystemManager.instance.SystemState == GameState.PlacePlane)
        {
            Debug.Log(SystemManager.instance.SystemState);
            SpawningManager.instance.SpawnPlane(SystemManager.instance.currentObjectToSpawn);
        }
        else
        {
            Debug.Log(SystemManager.instance.SystemState);
            SpawningManager.instance.SpawnObjectByIndicator(SystemManager.instance.currentObjectToSpawn);
            
        }
    }

    public void FinishPutPlane()
    {
        SimpleSound.instance.PlayButton();
        SystemManager.instance.FinishPlaneInitialization();
        finishPutPlaneButton.gameObject.SetActive(false);
    }
}
