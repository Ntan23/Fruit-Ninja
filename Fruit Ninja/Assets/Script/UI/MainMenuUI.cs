using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private Button playButton;
    [SerializeField] private Button trailSelectorButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject trailSelectorUI;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.DifficultySelector);
        });

        trailSelectorButton.onClick.AddListener(() => {
            trailSelectorUI.SetActive(true);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
