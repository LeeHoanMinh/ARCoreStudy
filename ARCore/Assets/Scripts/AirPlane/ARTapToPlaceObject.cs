using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject previewPlaneIndicator;
    public GameObject previewState;
    public GameObject tower;



    public GameObject title;
    public GameObject UIcanvas;
    public GameObject RealPlane;
    
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    public bool planeIsAffected = false;
    
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        previewState.SetActive(false);
        //PlaceObject(tower);

    }

    void Update()
    {

        UpdatePlacementPose();
        UpdatePlacementIndicator();
        UpdatePlaceObject();
    }

    public void TogglePlaneAffect()
    {
        previewState.SetActive(!previewState.active);
        planeIsAffected = !planeIsAffected;
        GameObject tmp = placementIndicator;
        placementIndicator = previewPlaneIndicator;
        previewPlaneIndicator = tmp;
    }
    void UpdatePlaceObject()
    {
        if (planeIsAffected)
        {       
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    RealPlane = PlaceObject(planeToPlace);
                    RealPlane.transform.localScale = placementIndicator.transform.localScale;


            }
        }
        else if (RealPlane != null)
        {
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && RealPlane.GetComponent<DefaultPlane>().CheckObjectInPlaneRadius(placementIndicator.transform.position))
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    PlaceObject(objectToPlace);
            }
        }
    }
 
    GameObject PlaceObject(GameObject objectToPlace)
    {
        GameObject newObject;
        newObject = Instantiate(objectToPlace);

        newObject.transform.position = new Vector3(placementPose.position.x, placementPose.position.y + newObject.transform.position.y, placementPose.position.z);
        
        //Place Title
        GameObject newTitle;
        newTitle = Instantiate(title);
        newTitle.transform.SetParent(UIcanvas.transform);
        newTitle.transform.position = newObject.transform.position + new Vector3(0f, 0.1f, 0f);
        if(RealPlane != null)
            newTitle.GetComponentInChildren<Text>().text = Vector3.Distance(placementIndicator.transform.position, RealPlane.transform.position).ToString();
        newTitle.SetActive(true);
        return newObject;
    }

    public void PlaceTower()
    {
        PlaceObject(tower);
    }

    void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            if (RealPlane != null)
            {
                DefaultPlane realPlaneManager;
                realPlaneManager = RealPlane.GetComponent<DefaultPlane>();
                if (realPlaneManager.CheckObjectInPlaneRadius(placementIndicator.transform.position))
                    placementIndicator.transform.position += new Vector3(0, realPlaneManager.transform.localScale.y + 0.00001f, 0);
            }
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
