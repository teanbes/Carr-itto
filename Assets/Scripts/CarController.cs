using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private CarStatsSO carStatsSO;
    [SerializeField] private GameObject car;
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform carCenterOfMass;

    [SerializeField] private InputManager inputManager;

    // Aditional Car Stats
    [SerializeField] private const float maxSpeed = 200;
    [SerializeField] private AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0, maxSpeed), new Keyframe(maxSpeed, 0));
    [SerializeField] private AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);
    private bool isGrounded = false;
    
    //Steering
    private float totalSteering;
    [HideInInspector] public float Steering { get => totalSteering; set => totalSteering = Mathf.Clamp(value, -1f, 1f);}
     
    //Drift
    [HideInInspector] public bool isDrifting = true;
    bool drift;
    public bool Drift { get => drift;  set => drift = value; }

    [Header("Wheels")]
    [SerializeField] private WheelCollider[] rearWheels;
    [SerializeField] private WheelCollider[] turnWheels;
    internal WheelCollider[] wheels;


    // Start is called before the first frame update
    private void Start()
    {
        carRB = GetComponent<Rigidbody>();
    }

    public void OnAccelerate()
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        inputManager.DriftEvent += OnDrift;
    }

    private void OnDisable()
    {
        inputManager.DriftEvent -= OnDrift;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Aceleracion: " + inputManager.accelerationInput);

        Debug.Log("steering: " + inputManager.steeringDirection);

        
    }

    public void OnDrift()
    {

        Debug.Log("Drifting: ");
    }

}
