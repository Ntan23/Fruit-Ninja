using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float timer = 0.0f;
    private GameManager gm;
    private SpawnManager spawnManager;

    void Awake()
    {
        gm = GameManager.Instance;
        spawnManager = SpawnManager.Instance;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnManager.maxLifetime) ResetBomb();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) gm.GameOver();
    }

    void ResetBomb()
    {
        transform.position = Vector3.zero;

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        gameObject.SetActive(false);

        timer = 0.0f;
    }
}
