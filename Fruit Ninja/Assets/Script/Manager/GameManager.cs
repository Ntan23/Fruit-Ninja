using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    #endregion

    #region Variables
    private int score;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private Blade blade;
    private SpawnManager spawnManager;
    #endregion

    void Start()
    {
        spawnManager = SpawnManager.Instance;

        NewGame();
    }

    public void NewGame()
    {
        score = 0;
    }

    public void AddScore()
    {
        score++;
        scoreUI.UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        blade.enabled = false;
        spawnManager.enabled = false;
    }
}
