using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
   /* int health;
    int maxHealth;
    float delayShoot;
    public float speed;

    private void Start()
    {
        enemyHealthBar = Instantiate(ObjectsManager.instance.enemyHeathBar);
        enemyHealthBar.transform.SetParent(GameObject.Find("WorldSpaceCanvas").transform);
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
        GameObject minus = Instantiate(ObjectsManager.instance.minusHealth, enemyHealthBar.transform);
        if(health == 0)
        {
            ScoreManager.instance.currentScore++;
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
            if (Vector3.Distance(this.transform.position, mainBuilding.transform.position) >= 0.3f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, mainBuilding.transform.position, speed);
            }
            else
            {

                if (delayShoot > 3f)
                {
                    delayShoot = 0;
                    mainBuilding.BeShot();
                    SimpleSound.instance.PlayEnemyShoot();
                }
            }
            delayShoot += Time.deltaTime;
        }
    }
    */
}
