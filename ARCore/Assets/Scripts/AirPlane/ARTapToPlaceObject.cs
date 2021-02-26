using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject planeToPlace;
    public GameObject placementIndicator;
    public GameObject title;
    public GameObject UIcanvas;
    public GameObject OnlyPlane;
    
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    public bool planeIsAffected = false;
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        

    }

    void Update()
    {

        UpdatePlacementPose();
        UpdatePlacementIndicator();
        if (planeIsAffected)
        {
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    OnlyPlane = PlaceObject(planeToPlace);
            }
        }
        else if(OnlyPlane != null)
        {
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    PlaceObject(objectToPlace);
            }
        }
    }
    
    bool CheckInZone()
    {
        float x = OnlyPlane.transform.position.x - placementPose.position.x;
        float z = OnlyPlane.transform.position.z - placementPose.position.z;
        float xz = Mathf.Sqrt(x * x + z * z);
        return xz <= 10f;
    }
    GameObject PlaceObject(GameObject objectToPlace)
    {
        GameObject newObject;
        newObject = Instantiate(objectToPlace);

        newObject.transform.position = new Vector3(placementPose.position.x, placementPose.position.y + newObject.transform.position.y, placementPose.position.z);
        //Place Title
        //GameObject newTitle;
        //newTitle = Instantiate(title);
        //newTitle.transform.SetParent(UIcanvas.transform);
        //newTitle.transform.position = newObject.transform.position + new Vector3(0f, 0.05f, 0f);
        //newTitle.SetActive(true);
        return newObject;
    }

    void UpdatePlacementIndicator()
    {
        if(placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }
}
