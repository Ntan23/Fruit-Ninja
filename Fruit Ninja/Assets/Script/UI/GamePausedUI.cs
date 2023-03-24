using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button gamePausedButton;
    [SerializeField] private GameObject settingsUI;
    private GameManager gm;
    #endregion
 
    void Start()
    {
        gm = GameManager.Instance;

        resumeButton.onClick.AddListener(() => {
            gm.ResumeGame();
            gamePausedButton.gameObject.SetActive(true);
        });

        settingsButton.onClick.AddListener(() => {
            settingsUI.SetActive(true);
        });

        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1.0f;
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });

        gamePausedButton.onClick.AddListener(() => {
            gm.PauseGame();
            gamePausedButton.gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }
}
