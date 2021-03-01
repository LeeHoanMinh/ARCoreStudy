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

        newObject = Instantiate(objectToSpawn);

        newObject.transform.position = objectPosition + originTranslate.position;
        newObject.transform.rotation = originTranslate.rotation * newObject.transform.rotation;
    }

    public void SpawnObjectByIndicator(GameObject objectToSpawn)
    {
        GameObject newObject;

        newObject = Instantiate(objectToSpawn,IndicatorManager.instance.PlacementIndicator.transform.position,IndicatorManager.instance.PlacementIndicator.transform.rotation * objectToSpawn.transform.rotation);
    }

    public void SpawnPlane(GameObject plane)
    {
        if (SystemManager.instance.currentPlane != null)
        {
            GameObject.Destroy(SystemManager.instance.currentPlane);
        }

        originTranslate = IndicatorManager.instance.PlacementPose;

        GameObject newObject;
        newObject = Instantiate(plane, originTranslate.position, originTranslate.rotation);
        newObject.transform.localScale = IndicatorManager.instance.PlacementIndicator.GetComponentInChildren<DefaultPlane>().transform.localScale;

        SystemManager.instance.currentPlane = newObject.GetComponent<DefaultPlane>();
        
    }


    public Vector3 TransformPositionToRealWorld(Vector3 position)
    {
        return position + originTranslate.position;
    }

    public Vector3 TransformPositionToUnity(Vector3 position)
    {
        return position - originTranslate.position;
    }

    public Quaternion TransformRotationToRealWorld(Quaternion rotation)
    {
        return originTranslate.rotation * rotation;
    }

    public Quaternion TransformRotationToUnity(Quaternion rotation)
    {
        return rotation * Quaternion.Inverse(originTranslate.rotation);
    }
}
