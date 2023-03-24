using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour
{
    bool isFirstUpdate = true;

    // Update is called once per frame
    void Update()
    {
        if(isFirstUpdate)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        isFirstUpdate = false;
        yield return new WaitForSeconds(1.0f);
        SceneLoader.SceneLoaderCallback();
    }
}
