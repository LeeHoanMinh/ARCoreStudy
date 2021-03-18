using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane3 : EnemyClass
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        TargetBuilding();
    }
}
