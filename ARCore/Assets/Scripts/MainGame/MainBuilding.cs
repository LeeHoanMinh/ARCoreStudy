    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBuilding : MonoBehaviour
{
    [SerializeField]
    int currentHealth;
    [SerializeField]
    int maxHealth;


    GameObject healthBarInstance;
    HealthBar healthBar;

    private void Start()
    {
        healthBarInstance = Instantiate(ObjectsManager.instance.enemyHealthBarPrefab);
        healthBarInstance.transform.SetParent(GameObject.Find("WorldSpaceCanvas").transform);
        healthBar = healthBarInstance.GetComponent<HealthBar>();
    }

    public void BeShot(int enemyDame)
    {
        currentHealth--;
        healthBar.PopUpHealthDecreaseText(enemyDame);
        if (currentHealth == 0)
        {
            Destroy(healthBarInstance);
            Destroy(this.gameObject);
            Canvas2DManager.instance.LoseBoard.SetActive(true);
        }
    }
    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.UpdateFilledAmount(currentHealth, maxHealth);
        healthBar.UpdatePosition(this.transform.position, 0.15f);
    }
}
