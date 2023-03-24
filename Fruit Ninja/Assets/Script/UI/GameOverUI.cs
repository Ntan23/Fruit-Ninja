using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    #region Variables
    private int difficultyIndex;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    private GameManager gm;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        difficultyIndex = PlayerPrefs.GetInt("Difficulty");

        retryButton.onClick.AddListener(() => {
            SceneLoader.ReloadScene();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });

        retryButton.interactable = false;
        mainMenuButton.interactable = false;
    }

    public void ShowGameOverUI()
    {
        void UpdateAlpha(float value)
        {
            GetComponent<CanvasGroup>().alpha = value;

            if(GetComponent<CanvasGroup>().alpha == 1) 
            {
                retryButton.interactable = true;
                mainMenuButton.interactable = true;
            }
        }

        LeanTween.value(gameObject, UpdateAlpha, 0.0f, 1.0f, 1.0f);
        scoreText.text = "Score : " + gm.GetScore().ToString();

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
