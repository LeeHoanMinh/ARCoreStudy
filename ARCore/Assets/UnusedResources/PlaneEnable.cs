using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PlaneEnable : MonoBehaviour
{
    [SerializeField]
    Button toggle;
    [SerializeField]
    Button affectPlan;
    [SerializeField]
    ARTapToPlaceObject manager;
    [SerializeField]
    ARPlaneManager arPlaneManager;
    void Start()
    {
        if (toggle != null)
        {
            toggle.onClick.AddListener(TogglePlaneDetection);
        }
        if(affectPlan != null)
        {
            affectPlan.onClick.AddListener(AffectPlane);
        }
      
    }

    void AffectPlane()
    {
        manager.TogglePlaneAffect();
        
        if(manager.planeIsAffected)
        {
            affectPlan.GetComponentInChildren<Text>().text = "Affect Plan: On";
        }
        else
        {
            affectPlan.GetComponentInChildren<Text>().text = "Affect Plan: Off";
        }
    }
    
    void TogglePlaneDetection()
    {
        arPlaneManager.enabled = !arPlaneManager.enabled;
        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(arPlaneManager.enabled);
        }
        if (arPlaneManager.enabled)
        {
            toggle.GetComponentInChildren<Text>().text = "Disable AR Plane Detection";
        }
        else
        {
            toggle.GetComponentInChildren<Text>().text = "Enable AR Plane Detection";
        }
    }

}
