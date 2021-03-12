using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass: MonoBehaviour
{
    [SerializeField]
    protected int currentHealth;
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected int dame;
    [SerializeField]
    protected float shootingSpeed;
    [SerializeField]
    protected float movingSpeed;
    [SerializeField]
    protected float shootingRange;



    GameObject healthBarInstance;
    HealthBar healthBar;
    float shootDelay;
    protected virtual void Start()
    {
        healthBarInstance = Instantiate(ObjectsManager.instance.enemyHealthBarPrefab);
        healthBarInstance.transform.SetParent(GameObject.Find("WorldSpaceCanvas").transform);
        healthBar = healthBarInstance.GetComponent<HealthBar>();
    }

    protected virtual void Update()
    {
        UpdateHealthBar();
    }

    protected void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        healthBarInstance.transform.position = this.transform.position + new Vector3(0f, 0.05f, 0f);
    }

    protected void TargetBuilding()
    {
        MainBuilding mainBuilding = SystemManager.instance.mainBuilding;

        if (mainBuilding != null)
        {
            float distanceWithMainBuilding = Vector3.Distance(this.transform.position, mainBuilding.transform.position);
            if (distanceWithMainBuilding >= shootingRange)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, mainBuilding.transform.position, movingSpeed);
            }
            else
            {
                if (shootDelay > shootingSpeed)
                {
                    shootDelay = 0;
                    //should be fixed
                    mainBuilding.BeShot(dame);
                    SimpleSound.instance.PlayEnemyShoot();
                    //here
                    
                }
            }
            shootDelay += Time.deltaTime;
        }
    }

    public void BeShot(int playerDame)
    {
        currentHealth--;
        healthBar.PopUpHealthDecreaseText(playerDame);
        if (currentHealth == 0)
        {
            ScoreManager.instance.currentScore++;
            Destroy(healthBarInstance);
            Destroy(this.gameObject);
        }
    }
}
