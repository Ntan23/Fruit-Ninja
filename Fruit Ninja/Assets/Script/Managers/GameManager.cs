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

    #region IntegerVariables
    private int score;
    private int livesCount;
    private int difficultyIndex;
    #endregion

    #region OtherVariables
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private Blade blade;
    [SerializeField] private Flashbang flashbang;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private LivesCountUI livesCountUI;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private GameObject gamePausedUI;
    [SerializeField] private GameObject gamePauseButton;
    private SpawnManager spawnManager;
    private AudioManager audioManager;
    #endregion

    void Start()
    {
        spawnManager = SpawnManager.Instance;
        audioManager = AudioManager.Instance;

        difficultyIndex = PlayerPrefs.GetInt("Difficulty");

        NewGame();
    }

    public void NewGame()
    {
        score = 0;
        livesCount = 3;
        scoreUI.UpdateScoreUI();

        blade.enabled = true;
        spawnManager.enabled = true;
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
        if(difficultyIndex == 0) 
        {
            if(score > PlayerPrefs.GetInt("EasyBestScore",0)) PlayerPrefs.SetInt("EasyBestScore",score);
        }

        if(difficultyIndex == 1) 
        {
            if(score > PlayerPrefs.GetInt("NormalBestScore",0)) PlayerPrefs.SetInt("NormalBestScore",score);
        }

        if(difficultyIndex == 2) 
        {
            if(score > PlayerPrefs.GetInt("HardBestScore",0)) PlayerPrefs.SetInt("HardBestScore",score);
        }
    }

    public void LoseLive()
    {
        livesCount--;
        livesCountUI.UpdateLivesUI(livesCount);
        cameraShake.CameraShakes();
        audioManager.Play("Hit");
        CheckLivesCount();
    }

    public void CheckLivesCount()
    {
        if(livesCount <= 0) GameOver();
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
        livesCountUI.gameObject.SetActive(false);
        gamePauseButton.SetActive(false);
        
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

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        gamePausedUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        gamePausedUI.SetActive(false);
    }
}
