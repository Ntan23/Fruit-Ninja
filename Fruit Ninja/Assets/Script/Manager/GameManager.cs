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
    [SerializeField] private Flashbang flashbang;
    [SerializeField] private GameOverUI gameOverUI;
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
        scoreUI.UpdateScoreUI();

        blade.enabled = true;
        spawnManager.enabled = true;

        ClearScene();
    }

    public void AddScore(int value)
    {
        score += value;
        scoreUI.UpdateScoreUI();
        CheckScore();
        scoreUI.UpdateBestScore();
    }

    public void CheckScore()
    {
        if(score > PlayerPrefs.GetInt("BestScore",0)) PlayerPrefs.SetInt("BestScore",score);
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        blade.enabled = false;
        spawnManager.enabled = false;
        scoreUI.gameObject.SetActive(false);
        
        ClearScene();
        StartCoroutine(ShowGameOverUI());
    }

    void ClearScene()
    {
        Fruit[] fruitsInScene = FindObjectsOfType<Fruit>();

        foreach(Fruit fruit in fruitsInScene)
        {
            fruit.gameObject.SetActive(false);
        }

        Bomb[] bombsInScene = FindObjectsOfType<Bomb>();

        foreach(Bomb bomb in bombsInScene)
        {
            bomb.gameObject.SetActive(false);
        }
    }

    IEnumerator ShowGameOverUI()
    {
        flashbang.FlashbangOn();
        yield return new WaitForSeconds(1.0f);
        gameOverUI.ShowGameOverUI();
    }
}
