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
    [SerializeField]
    protected int expValue;



    GameObject healthBarInstance;
    protected HealthBar healthBar;
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
        healthBar.UpdateFilledAmount(currentHealth, maxHealth);
        UpdateHealthBarPosition();
    }

    protected virtual void UpdateHealthBarPosition()
    {
        healthBar.UpdatePosition(this.transform.position, 0.05f);
    }

    protected void TargetBuilding()
    {
        MainBuilding mainBuilding = SystemManager.instance.mainBuilding;
        
        if (mainBuilding != null)
        {
            this.transform.LookAt(mainBuilding.transform);
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
        currentHealth -= playerDame;
        healthBar.PopUpHealthDecreaseText(playerDame);
        if (currentHealth <= 0)
        {
            ScoreManager.instance.currentScore++;
            Player.instance.PlusExp(expValue);
            Destroy(healthBarInstance);
            Destroy(this.gameObject);
        }
    }
}
