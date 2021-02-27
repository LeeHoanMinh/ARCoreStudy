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
    }

    void PlaceObject()
    {
        SpawningManager.instance.SpawnPlane(SystemManager.instance.currentObjectToSpawn);
    }
}
