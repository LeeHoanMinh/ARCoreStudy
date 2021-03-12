using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Image filledPart;
    [SerializeField]
    GameObject healthDecreasedText;

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        filledPart.fillAmount = (float)currentHealth / maxHealth;
    }

    public void PopUpHealthDecreaseText(int decreasedHealth)
    {
        HealthDecreasedText decreasedText = Instantiate(healthDecreasedText, this.transform).GetComponent<HealthDecreasedText>();
        decreasedText.SetDecreasedHealth(decreasedHealth);
    }
}
