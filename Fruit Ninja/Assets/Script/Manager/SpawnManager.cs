using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Singleton
    public static SpawnManager Instance {get; private set;}
    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    #endregion

    #region FloatVariables
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float maxLifetime;
    [Range(0f,1.0f)]
    [SerializeField] private float bombChance;
    #endregion

    #region OtherVariables
    private Collider spawnArea;
    [Header("Object To Spawn")]
    [SerializeField] private GameObject[] fruits;
    [SerializeField] private GameObject bomb;
    #endregion

    void Start()
    {
        spawnArea = GetComponent<Collider>();
    }

    void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);

        while (enabled)
        {
            GameObject objectToSpawn = fruits[Random.Range(0, fruits.Length)];

            if(bombChance > Random.value) objectToSpawn = bomb;

            Vector3 spawnPosition =  new Vector3();
            spawnPosition.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            spawnPosition.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            spawnPosition.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            float angle = Random.Range(minAngle, maxAngle);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, angle);

            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            float spawnForce = Random.Range(minForce, maxForce);
            spawnedObject.GetComponent<Rigidbody>().AddForce(spawnedObject.transform.up * spawnForce, ForceMode.Impulse);

            Destroy(spawnedObject, maxLifetime);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSpawnDelay));
        }
    }
}
