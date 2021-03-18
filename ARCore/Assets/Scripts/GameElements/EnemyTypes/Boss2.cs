using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : EnemyClass
{
    [SerializeField] 
    float timeToHealth;

    float currentTimeToHealth;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        TargetBuilding();
        CheckTimeToHealth();
    }

    protected override void UpdateHealthBarPosition()
    {
        healthBar.UpdatePosition(this.transform.position, 0.15f);
    }

    void CheckTimeToHealth()
    {
        currentTimeToHealth += Time.deltaTime;
        if(currentTimeToHealth >= timeToHealth)
        {
            HealthAllEnemy();
            currentTimeToHealth = 0;
        }
    }
    void HealthAllEnemy()
    {
        //Implement to health all enemy
    }
}
