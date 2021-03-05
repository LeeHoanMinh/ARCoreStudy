using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int currentScore = 0;
    public Text ScoreText;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(ScoreText != null)
            ScoreText.text = "Score: " + currentScore.ToString();
    }
}
