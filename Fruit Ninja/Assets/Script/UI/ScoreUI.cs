using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    GameManager gm;
    #endregion

    void Awake()
    {
        gm = GameManager.Instance;

        UpdateBestScore();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score : " + gm.GetScore().ToString();
    }

    public void UpdateBestScore()
    {
        bestScoreText.text = "Best : " + PlayerPrefs.GetInt("BestScore",0).ToString();
    }
}
