using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SciFiArsenal;

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
        if (ModeManager.instance.InEditorMode())
            movingSpeed /= 5f;
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
                    ShootAnimation();
                    //here
                    
                }
            }
            shootDelay += Time.deltaTime;
        }
    }


    void ShootAnimation()
    {
        GameObject projectile = Instantiate(ObjectsManager.instance.enemyProjectile, this.transform.position, Quaternion.identity) as GameObject;
        projectile.transform.LookAt(SystemManager.instance.mainBuilding.transform);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 10f);
       // projectile.GetComponent<SciFiProjectileScript>().impactNormal = hitObject[0].normal;
    }
    public void BeShot(int playerDame)
    {
        currentHealth -= playerDame;
        healthBar.PopUpHealthDecreaseText(playerDame);
        if (currentHealth <= 0)
        {
            Instantiate(ObjectsManager.instance.explosion,this.transform.position,Quaternion.identity);
            ScoreManager.instance.currentScore++;
            Player.instance.PlusExp(expValue);
            Destroy(healthBarInstance);
            Destroy(this.gameObject);
        }
    }
}
