using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;

    public GameObject currentObjectToSpawn;
    public DefaultPlane currentPlane;
    public MainBuilding mainBuilding;
    GameState systemState;
    public GameState SystemState
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
        systemState = GameState.PlacePlane;
        IndicatorManager.instance.SetPlacementIndicatorById(0);
        currentObjectToSpawn = ObjectsManager.instance.ground;
    }

    public void FinishPlaneInitialization()
    {
        systemState = GameState.StartGame;
        IndicatorManager.instance.SetPlacementIndicatorByDefault();

        SpawningManager.instance.SpawnBuilding(ObjectsManager.instance.building);
        //mainBuilding.BuildingSetUp(30);
        currentObjectToSpawn = ObjectsManager.instance.airplane;
    }
}

