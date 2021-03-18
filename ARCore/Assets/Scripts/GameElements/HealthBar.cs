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
    [SerializeField]
    bool isScaleWithSize;

    int preHealth = -1;
    public void UpdateFilledAmount(int currentHealth, int maxHealth)
    {
        filledPart.fillAmount = (float)currentHealth / maxHealth;
        if (maxHealth != preHealth)
        {
            preHealth = maxHealth;
            if(isScaleWithSize)
                ChangeHealthBarSize();
        }
    }

    void ChangeHealthBarSize()
    {
        RectTransform rect = this.GetComponent<RectTransform>();
        float newWidth = rect.sizeDelta.x;
        if (preHealth > 10)
        {
            newWidth += (float)(Mathf.Min(100,preHealth) - 10) * 100/90;   
        }
        if (preHealth > 100)
        {
            newWidth += (float)(Mathf.Min(1000,preHealth) - 100) * 200 / 900;
        }

        rect.sizeDelta = new Vector2(newWidth, rect.sizeDelta.y);
    }

    public void UpdatePosition(Vector3 origin, float height)
    {
        this.transform.position = origin + new Vector3(0f, height, 0f);
    }

    public void PopUpHealthDecreaseText(int decreasedHealth)
    {
        HealthDecreasedText decreasedText = Instantiate(healthDecreasedText, this.transform).GetComponent<HealthDecreasedText>();
        decreasedText.SetDecreasedHealth(decreasedHealth);
    }
}
