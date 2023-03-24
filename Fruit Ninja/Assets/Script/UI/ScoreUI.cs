using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    #region Variables
    private int difficultyIndex;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    GameManager gm;
    #endregion

    void Awake()
    {
        gm = GameManager.Instance;

        difficultyIndex = PlayerPrefs.GetInt("Difficulty");

        UpdateBestScore();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score : " + gm.GetScore().ToString();
    }

    public void UpdateBestScore()
    {
        if(difficultyIndex == 0)
        {
            bestScoreText.text = "Best : " + PlayerPrefs.GetInt("EasyBestScore",0).ToString();
        }   

        if(difficultyIndex == 1)
        {
            bestScoreText.text = "Best : " + PlayerPrefs.GetInt("NormalBestScore",0).ToString();
        }   

        if(difficultyIndex == 2)
        {
            bestScoreText.text = "Best : " + PlayerPrefs.GetInt("HardBestScore",0).ToString();
        }   
    }
}
