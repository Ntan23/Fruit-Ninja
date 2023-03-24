using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectorUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private Button backButton;
    [SerializeField] private Button easyModeButton;
    [SerializeField] private Button normalModeButton;
    [SerializeField] private Button hardModeButton;
    #endregion

    void Start()
    {
        backButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });

        easyModeButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Difficulty",0);
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        normalModeButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Difficulty",1);
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        hardModeButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt("Difficulty",2);
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });
    }
}
