using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class IndicatorManager : MonoBehaviour
{
    public static IndicatorManager instance;
    bool isActive = false;
 
    ARSessionOrigin arSessionOrigin;

    GameObject placementIndicator;
    public GameObject PlacementIndicator
    {
        get { return placementIndicator; }
    }

    public GameObject defaultIndicatorType;

    public GameObject[] indicatorTypes;

    Pose placementPose;
    bool placementPoseIsValid = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        placementIndicator = Instantiate(defaultIndicatorType);   
    }

    void Start()
    {
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();    
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            DefaultPlane currentPlane = SystemManager.instance.currentPlane;
            if (currentPlane != null)
            {
                if (currentPlane.CheckObjectInPlaneRadius(placementIndicator.transform.position))
                {
                    //Set Indicator On Plane;
                    placementIndicator.transform.position += new Vector3(0f, currentPlane.transform.position.y + 0.0001f,0f);
                }
            }
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }



    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arSessionOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    public void SetPlacementIndicatorById(int id)
    {
        GameObject tmp = placementIndicator;
        placementIndicator = Instantiate(indicatorTypes[id]);
        GameObject.Destroy(tmp);
    }

    public void ResizePlacementIndicator(Vector3 newSize)
    {
        placementIndicator.transform.localScale = newSize;
    }
}
