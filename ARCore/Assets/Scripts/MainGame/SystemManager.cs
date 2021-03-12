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
        IndicatorManager.instance.isActive = false;

        SpawningManager.instance.SpawnBuilding(ObjectsManager.instance.building);


        
        ScoreManager.instance.ScoreText = Instantiate(ObjectsManager.instance.ScoreText, GameObject.Find("WorldSpaceCanvas").transform).GetComponent<Text>();
        ScoreManager.instance.ScoreText.transform.position = mainBuilding.transform.position + new Vector3(0f, 0.01f, 0.3f);
        
        currentObjectToSpawn = ObjectsManager.instance.airplane;


        StartCoroutine(spawnEnemy());

    }
    IEnumerator spawnEnemy()
{
        enemySpawner = Instantiate(ObjectsManager.instance.enemySpawner[0]).GetComponent<EnemySpawner>();
        enemySpawner.transform.position = mainBuilding.transform.position + new Vector3(0.4f, 0.4f, 0.4f);
        enemySpawner.transform.LookAt(mainBuilding.transform);
        enemySpawner.StartSpawning();
        yield return new WaitForSeconds(40f);

        enemySpawner = Instantiate(ObjectsManager.instance.enemySpawner[1]).GetComponent<EnemySpawner>();
        enemySpawner.transform.position = mainBuilding.transform.position + new Vector3(-0.4f, 0.4f, -0.4f);
        enemySpawner.transform.LookAt(mainBuilding.transform);
        enemySpawner.StartSpawning();
        yield return new WaitForSeconds(40f);

        enemySpawner = Instantiate(ObjectsManager.instance.enemySpawner[2]).GetComponent<EnemySpawner>();
        enemySpawner.transform.position = mainBuilding.transform.position + new Vector3(0.4f, 0.4f, 0.4f);
        enemySpawner.transform.LookAt(mainBuilding.transform);
        enemySpawner.StartSpawning();
        yield return new WaitForSeconds(50f);

        enemySpawner = Instantiate(ObjectsManager.instance.enemySpawner[3]).GetComponent<EnemySpawner>();
        enemySpawner.transform.position = mainBuilding.transform.position + new Vector3(-0.4f, 0.4f, 0.4f);
        enemySpawner.transform.LookAt(mainBuilding.transform);
        enemySpawner.StartSpawning();
        yield return new WaitForSeconds(50f);

        enemySpawner = Instantiate(ObjectsManager.instance.enemySpawner[4]).GetComponent<EnemySpawner>();
        enemySpawner.transform.position = mainBuilding.transform.position + new Vector3(0.4f, 0.4f, 0.4f);
        enemySpawner.transform.LookAt(mainBuilding.transform);
        enemySpawner.StartSpawning();
        yield return new WaitForSeconds(60f);

        enemySpawner = Instantiate(ObjectsManager.instance.enemySpawner[5]).GetComponent<EnemySpawner>();
        enemySpawner.transform.position = mainBuilding.transform.position + new Vector3(-0.4f, 0.4f, -0.4f);
        enemySpawner.transform.LookAt(mainBuilding.transform);
        enemySpawner.StartSpawning();
        yield return new WaitForSeconds(40f);

        yield return null;
    }
}

