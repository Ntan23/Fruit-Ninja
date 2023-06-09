using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    #region BoolVariables
    private bool isSlicing;
    [SerializeField] private bool useMobile;
    #endregion

    #region FloatVariables
    public float minSliceVelocity;
    public float sliceForce;
    #endregion

    #region VectorVariables
    public Vector3 bladeDirection {get; private set;}
    #endregion

    #region OtherVariables
    private Collider bladeCollider;
    private Camera mainCam;
    private TrailRenderer bladeTrail;
    [SerializeField] private Gradient[] trailGradient;
    #endregion

    void Awake()
    {
        mainCam = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    void Start()
    {
        bladeTrail.colorGradient = trailGradient[PlayerPrefs.GetInt("SelectedTrail")];
    }

    void OnEnable()
    {
        StopSlicing();
    }

    void Update()
    {
        if(!useMobile)
        {
            if(Input.GetMouseButtonDown(0)) StartSlicing();
            else if(Input.GetMouseButtonUp(0)) StopSlicing();
        }
        else if(useMobile)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began) StartSlicing();
                else if(touch.phase == TouchPhase.Ended) StopSlicing();
            }
        }
        
        if(isSlicing) ContinueSlicing();
    }

    void OnDisable()
    {
        StopSlicing();
    }

    void StartSlicing()
    {
        Vector3 newBladePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        newBladePosition.z = 0f;

        transform.position = newBladePosition;

        isSlicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    void StopSlicing()
    {
        isSlicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    void ContinueSlicing()
    {
        Vector3 newBladePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        newBladePosition.z = 0f;

        bladeDirection = newBladePosition - transform.position;

        float velocity = bladeDirection.magnitude / Time.deltaTime;

        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newBladePosition;
    }
}
