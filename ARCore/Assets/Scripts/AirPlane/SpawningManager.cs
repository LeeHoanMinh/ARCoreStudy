using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    public static SpawningManager instance;
    
    Vector3 originTranslate;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SpawnObject(GameObject objectToSpawn)
    {
        GameObject newObject;
        newObject = Instantiate(objectToSpawn);
        newObject.transform.position += originTranslate;
    }
    
    public void SpawnPlane(GameObject plane)
    {
        originTranslate = IndicatorManager.instance.PlacementIndicator.transform.position;
        GameObject newObject;
        newObject = Instantiate(plane);
        newObject.transform.position += originTranslate;
        plane.transform.localScale = IndicatorManager.instance.PlacementIndicator.transform.localScale;
        SystemManager.instance.currentPlane = newObject.GetComponent<DefaultPlane>();
    }


}
