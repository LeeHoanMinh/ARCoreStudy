using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBuilding : MonoBehaviour
{
    [SerializeField]
    int health = 30;
    [SerializeField]
    int maxHealth = 30;
    GameObject buildingHealthBar;
    Image buildingHealthComponent;
    private void Start()
    {
        buildingHealthBar = Instantiate(ObjectsManager.instance.enemyHeathBar);
        buildingHealthBar.name = "BuildingHealthBar";
        buildingHealthBar.transform.parent = GameObject.Find("WorldSpaceCanvas").transform;
        buildingHealthComponent = buildingHealthBar.transform.GetChild(0).GetComponent<Image>();
    }

    public void BuildingSetUp(int _health)
    {
        maxHealth = _health;
        health = maxHealth;
        //Debug.Log(health);
        
    }
    public void BeShot()
    {
        health--;
        if (health == 0)
        {
            Destroy(buildingHealthBar);
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        buildingHealthComponent.fillAmount = (float)health / maxHealth;
        buildingHealthBar.transform.position = this.transform.position + new Vector3(0f, 0.15f, 0f);
    }
}
