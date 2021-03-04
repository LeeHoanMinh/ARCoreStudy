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
        originTranslate.position = new Vector3(0f, 0f, 0f);
        originTranslate.rotation = new Quaternion(0f, 0f, 0f, 0f);
 
        

        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        //SystemManager.instance.FinishPlaneInitialization();
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
            GameObject.Destroy(SystemManager.instance.currentPlane.gameObject);
        }

        originTranslate = IndicatorManager.instance.PlacementPose;
        Debug.Log(originTranslate);
        GameObject newObject;
        newObject = Instantiate(plane, originTranslate.position, originTranslate.rotation);
        newObject.transform.localScale = IndicatorManager.instance.PlacementIndicator.GetComponentInChildren<DefaultPlane>().transform.localScale;

        SystemManager.instance.currentPlane = newObject.GetComponent<DefaultPlane>();
        
    }

    public void SpawnBuilding(GameObject building)
    {
        Debug.Log("Spawn Building");
        GameObject newObject;
        newObject = Instantiate(building);
        newObject.transform.position = newObject.transform.position + originTranslate.position;
        newObject.transform.rotation = originTranslate.rotation * newObject.transform.rotation;
        Debug.Log(newObject);
        Debug.Log(newObject.transform.position);
        SystemManager.instance.mainBuilding = newObject.GetComponent<MainBuilding>();
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
