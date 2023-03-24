using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    #region Variables
    [SerializeField] private int scoreToAdd;
    private float timer;
    private bool isSliced;
    [SerializeField] private GameObject unslicedObj;
    [SerializeField] private GameObject slicedObj;
    [SerializeField] private GameObject slicedTopObj;
    [SerializeField] private GameObject slicedBottomObj;
    private Rigidbody rb;
    private Collider objCollider;
    private ParticleSystem particleEffect;
    private GameManager gm;
    private SpawnManager spawnManager;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        objCollider = GetComponent<Collider>();
        particleEffect = GetComponentInChildren<ParticleSystem>();

        gm = GameManager.Instance;
        spawnManager = SpawnManager.Instance;
    }

    void OnEnable()
    {
        particleEffect.Pause();

        timer = 0.0f;

        isSliced = false;
    }

    void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }  

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnManager.maxLifetime) 
        {
            if(isSliced) ResetFruit(); 
            else if(!isSliced) gameObject.SetActive(false);
        }
    }

    void Slice(Vector3 direction, Vector3 position, float force)
    {
        unslicedObj.SetActive(false);
        slicedObj.SetActive(true);

        //objCollider.enabled = false;
        isSliced = true;
        particleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slicedObj.transform.rotation = Quaternion.Euler(0, 0, angle);
    
        Rigidbody[] slicedObjRb = slicedObj.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody slicedRb in slicedObjRb)
        {
            slicedRb.velocity = rb.velocity;
            slicedRb.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            Blade blade = other.GetComponent<Blade>();

            if(!isSliced)
            {
                Slice(blade.bladeDirection, blade.transform.position, blade.sliceForce);
                gm.AddScore(scoreToAdd);
            }
        }
    }

    private void ResetFruit()
    {
        transform.position = Vector3.zero;

        unslicedObj.SetActive(true);

        slicedTopObj.transform.position = Vector3.zero;
        slicedBottomObj.transform.position = Vector3.zero;
        slicedBottomObj.transform.position = Vector3.zero;

        slicedTopObj.transform.rotation = Quaternion.Euler(-90, 0, 0);
        slicedBottomObj.transform.rotation = Quaternion.Euler(90, 0, 0);

        slicedTopObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        slicedBottomObj.GetComponent<Rigidbody>().velocity = Vector3.zero;

        slicedTopObj.SetActive(true);
        slicedBottomObj.SetActive(true);
        slicedObj.SetActive(false);

        gameObject.SetActive(false);
        isSliced = false;
    }
}
