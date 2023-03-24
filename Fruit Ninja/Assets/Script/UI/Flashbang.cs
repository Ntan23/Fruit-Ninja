using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashbang : MonoBehaviour
{
    public void FlashbangOn()
    {
        StartCoroutine(PlayFlashbang());
    }

    IEnumerator PlayFlashbang()
    {
        void UpdateAlpha(float value)
        {
            GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, value);
        }

        LeanTween.value(gameObject, UpdateAlpha, 0.0f, 1.0f, 0.4f);
        yield return new WaitForSeconds(0.8f);
        LeanTween.value(gameObject, UpdateAlpha, 1.0f, 0.0f, 0.4f);
    }
}
