using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyClass
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
