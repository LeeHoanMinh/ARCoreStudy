using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;

    public GameObject currentObjectToSpawn;
    public DefaultPlane currentPlane;
    public MainBuilding mainBuilding;
    public EnemySpawner enemySpawner;
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
        mainBuilding.BuildingSetUp(30);

        
        ScoreManager.instance.ScoreText = Instantiate(ObjectsManager.instance.ScoreText, GameObject.Find("WorldSpaceCanvas").transform).GetComponent<Text>();
        ScoreManager.instance.ScoreText.transform.position = mainBuilding.transform.position + new Vector3(0f, 0.01f, 0.3f);
        
        currentObjectToSpawn = ObjectsManager.instance.airplane;

        enemySpawner = Instantiate(ObjectsManager.instance.enemySpawner).GetComponent<EnemySpawner>();
        enemySpawner.transform.position = mainBuilding.transform.position + new Vector3(0.4f,0.4f,0.4f);
        enemySpawner.transform.LookAt(mainBuilding.transform);
        enemySpawner.StartSpawning();
    }
}

