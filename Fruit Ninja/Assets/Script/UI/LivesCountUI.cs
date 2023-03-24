using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCountUI : MonoBehaviour
{
    [SerializeField] private GameObject[] lives;

    public void UpdateLivesUI(int livesCount)
    {
        lives[livesCount].gameObject.SetActive(false);
    }
}
