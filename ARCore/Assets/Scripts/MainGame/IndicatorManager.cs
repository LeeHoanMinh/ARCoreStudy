using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class IndicatorManager : MonoBehaviour
{
    public static IndicatorManager instance;
    public bool isActive = true;
    
    ARSessionOrigin arSessionOrigin;
    Camera currentCamera;
    GameObject placementIndicator;
    public GameObject PlacementIndicator
    {
        get { return placementIndicator; }
    }

    public GameObject defaultIndicatorType;

    public GameObject[] indicatorTypes;

    Pose placementPose;
    public Pose PlacementPose
    {
        get { return placementPose; }
    }
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
        currentCamera = ModeManager.instance.GetMainCamera();
    }

    private void Update()
    {
        if (isActive == true)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }
        else placementIndicator.SetActive(false);
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
                    placementIndicator.transform.position += new Vector3(0f, currentPlane.transform.localScale.y,0f);
                }
            }
            placementIndicator.transform.position += new Vector3(0f, 0.001f, 0f);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }



    void UpdatePlacementPose()
    {
        if (ModeManager.instance.InEditorMode())
        {
            RaycastHit hit;
            Ray ray = currentCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.transform.gameObject.tag == "Plane")
                {
                    placementPoseIsValid = true;
                    placementPose.position = hit.point;
                }
            }
            else
                placementPoseIsValid = false;
        }
        else
        {
            var screenCenter = currentCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            arSessionOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.Planes);
            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid)
            {
                placementPose = hits[0].pose;
            }
        }

    }

    public void SetPlacementIndicatorById(int id)
    {
        GameObject tmp = placementIndicator;
        placementIndicator = Instantiate(indicatorTypes[id]);
        GameObject.Destroy(tmp);
    }

    public void SetPlacementIndicatorByDefault()
    {
        GameObject tmp = placementIndicator;
        placementIndicator = Instantiate(defaultIndicatorType);
        GameObject.Destroy(tmp);
    }
    public void ResizePlacementIndicator(Vector3 newSize)
    {
        placementIndicator.GetComponentInChildren<DefaultPlane>().ResizePlane(newSize);
    }
}
