using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;

    public GameObject currentObjectToSpawn;
    public DefaultPlane currentPlane;
    int systemState = 0;
    public int SystemState
    {
        get { return systemState; }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ActivatePlaneInitialization()
    {
        systemState = 1;
        IndicatorManager.instance.SetPlacementIndicatorById(0);
        currentObjectToSpawn = ObjectsManager.instance.objectToSpawn[0];
    }

    public void FinishPlaneInitialization()
    {
        systemState = 2;
        IndicatorManager.instance.SetPlacementIndicatorByDefault();
        currentObjectToSpawn = ObjectsManager.instance.objectToSpawn[1];
    }
}

