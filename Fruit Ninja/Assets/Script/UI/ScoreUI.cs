using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI scoreText;
    GameManager gm;
    #endregion

    void Start()
    {
        gm = GameManager.Instance;
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score : " + gm.GetScore().ToString();
    }
}
