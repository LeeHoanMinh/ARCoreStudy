using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawningManager : MonoBehaviour
{
    public static SpawningManager instance;

    Pose originTranslate;
    public Pose OriginalTranslate
    {
        get { return originTranslate; }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SpawnObjectByPosition(GameObject objectToSpawn, Vector3 objectPosition)
    {
        GameObject newObject;

        newObject = Instantiate(objectToSpawn, SystemManager.instance.planeAnchor.transform);
        newObject.transform.localPosition = objectPosition;
    }

    public void SpawnPlane(GameObject plane)
    {
        if (SystemManager.instance.currentPlane != null)
        {
            GameObject.Destroy(SystemManager.instance.planeAnchor);
        }

        GameObject newAnchor;
        
        originTranslate = IndicatorManager.instance.PlacementPose;
        newAnchor = Instantiate(ObjectsManager.instance.defaultAnchor, originTranslate.position, Quaternion.Inverse(originTranslate.rotation));
        
        GameObject newObject;
        newObject = Instantiate(plane,newAnchor.transform);
        newObject.transform.localScale = IndicatorManager.instance.PlacementIndicator.GetComponentInChildren<DefaultPlane>().transform.localScale;

        SystemManager.instance.planeAnchor = newAnchor;
        SystemManager.instance.currentPlane = newObject.GetComponent<DefaultPlane>();
        
    }

}
