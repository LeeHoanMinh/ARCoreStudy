using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    int health = 5;
    int maxHealth = 5;
    float delayShoot;
    GameObject enemyHealthBar;
    Image enemyHealthComponent;
    private void Start()
    {
        enemyHealthBar = Instantiate(ObjectsManager.instance.enemyHeathBar);
        enemyHealthBar.transform.parent = GameObject.Find("WorldSpaceCanvas").transform;
        enemyHealthComponent = enemyHealthBar.transform.GetChild(0).GetComponent<Image>();
    }

    public void EnemySetUp(int _health)
    {
        maxHealth = _health;
        health = maxHealth;
    }
    public void BeShot()
    {
        health--;
        if(health == 0)
        {
            Destroy(enemyHealthBar);
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        
        UpdateUIBar();
        TargetBuilding();
    }

    void UpdateUIBar()
    {
        if(maxHealth != 0)
            enemyHealthComponent.fillAmount = (float)health / maxHealth;
        enemyHealthBar.transform.position = this.transform.position + new Vector3(0f, 0.05f, 0f);
    }

    void TargetBuilding()
    {
    
        MainBuilding mainBuilding = SystemManager.instance.mainBuilding;

        if (mainBuilding != null)
        {
            if (Vector3.Distance(this.transform.position, mainBuilding.transform.position) >= 0.1f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, mainBuilding.transform.position, 0.0002f);
            }
            else
            {

                if (delayShoot > 3f)
                {
                    delayShoot = 0;
                    mainBuilding.BeShot();
                }
            }
            delayShoot += Time.deltaTime;
        }
    }
}
