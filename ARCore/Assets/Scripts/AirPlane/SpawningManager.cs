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
        Vector3 newObjectPosition = objectToSpawn.transform.position;
        Quaternion newObjectRotation = objectToSpawn.transform.rotation;
        newObjectPosition += objectPosition;

        newObject = Instantiate(objectToSpawn, newObjectPosition + originTranslate.position, originTranslate.rotation * newObjectRotation);
        
    }

    public void SpawnPlane(GameObject plane)
    {
        if (SystemManager.instance.currentPlane != null)
            GameObject.Destroy(SystemManager.instance.currentPlane.gameObject);
        originTranslate = IndicatorManager.instance.PlacementPose;
        GameObject newObject;
        newObject = Instantiate(plane, originTranslate.position, originTranslate.rotation);
        newObject.transform.localScale = IndicatorManager.instance.PlacementIndicator.GetComponentInChildren<DefaultPlane>().transform.localScale;
        SystemManager.instance.currentPlane = newObject.GetComponent<DefaultPlane>();
    }

}
