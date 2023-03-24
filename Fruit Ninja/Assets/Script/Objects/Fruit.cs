using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    #region Variables
    [SerializeField] private int scoreToAdd;
    private bool isSliced;
    [SerializeField] private GameObject unslicedObj;
    [SerializeField] private GameObject slicedObj;
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
        isSliced = false;
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

        foreach(Rigidbody rb in slicedObjRb)
        {
            rb.velocity = rb.velocity;
            rb.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
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
}
