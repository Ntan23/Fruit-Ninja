using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailSelectorUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Image checkmark;
    [SerializeField] private GameObject[] trails;

    void Start()
    {
        backButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
        checkmark.gameObject.SetActive(false);

        SelectTrail(PlayerPrefs.GetInt("SelectedTrail",0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTrail(int index)
    {
        checkmark.transform.SetParent(trails[index].transform);
        checkmark.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
        checkmark.gameObject.SetActive(true);

        PlayerPrefs.SetInt("SelectedTrail",index);
    }
}
