using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;

    public GameObject currentObjectToSpawn;
    public DefaultPlane currentPlane;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ActivatePlaneInitialization()
    {
        IndicatorManager.instance.SetPlacementIndicatorById(0);
        currentObjectToSpawn = ObjectsManager.instance.objectToSpawn[0];
    }
}

