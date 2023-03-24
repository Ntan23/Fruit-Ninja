using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        retryButton.onClick.AddListener(() => {
            SceneLoader.ReloadScene();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });
    }

    public void ShowGameOverUI()
    {
        void UpdateAlpha(float value)
        {
            GetComponent<CanvasGroup>().alpha = value;
        }

        LeanTween.value(gameObject, UpdateAlpha, 0.0f, 1.0f, 1.0f);
        scoreText.text = "Score : " + gm.GetScore().ToString();
        bestScoreText.text = "Best : " + PlayerPrefs.GetInt("BestScore").ToString();
    }
    
}
