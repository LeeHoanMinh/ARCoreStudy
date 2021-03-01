using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    int health = 5;
    GameObject enemyHealthBar;
    Image enemyHealthComponent;
    private void Start()
    {
        enemyHealthBar = Instantiate(ObjectsManager.instance.enemyHeathBar);
        enemyHealthBar.transform.parent = GameObject.Find("WorldSpaceCanvas").transform;
        enemyHealthComponent = enemyHealthBar.transform.GetChild(0).GetComponent<Image>();
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
       enemyHealthComponent.fillAmount = (float)health / 5;
       enemyHealthBar.transform.position = this.transform.position + new Vector3(0f, 0.05f, 0f);
    }

}
