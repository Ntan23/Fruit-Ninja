using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button trailSelectorButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject trailSelectorUI;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.GameScene);
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
