using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameScreen : UIBase
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    private int totalScore = 0;
    private static InGameScreen instance;
    public static InGameScreen Instance => instance ?? (instance = FindObjectOfType<InGameScreen>());
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void UpdateScore(int score)
    {
        totalScore += score;
        scoreText.SetText(totalScore.ToString());
    }

    public void UpdateWave(int wave)
    {
        waveText.SetText(wave.ToString());
    }

}
