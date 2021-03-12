using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDecreasedText : MonoBehaviour
{
    Text text;
    void Awake()
    {
        text = this.GetComponent<Text>();
    }
    void Start()
    {
        this.transform.position += new Vector3(0f, 0.01f, 0f); 
        StartCoroutine(DecreaseHealthAnimation());
    }

    IEnumerator DecreaseHealthAnimation()
    {
        for (int i = 0;i < 20;i++)
        {
            this.transform.position += new Vector3(0f, 0.001f, 0f);
            Color color = text.color;
            color.a -= 0.05f;
            text.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        GameObject.Destroy(this.gameObject);
    }

    public void SetDecreasedHealth(int decreasedHealth)
    {
        text.text = decreasedHealth.ToString();
    }
}
