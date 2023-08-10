using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CarStatsSO carStatsSO;
    [SerializeField] private GameObject car;
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform carCenterOfMass;
    [SerializeField] private InputManager inputManager;


    [Header("Aditional Car Stats")]
    [SerializeField] private const float maxSpeed = 200;
    [SerializeField] private AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0, maxSpeed), new Keyframe(maxSpeed, 0));
    [SerializeField] private AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);
    [SerializeField] private float speed = 3.6f;
    private float steering;
    private float currentSpeed;
    private bool isBreaking = false;
    private bool isGrounded = false;
    private IEnumerator coroutine;

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
        wheels = GetComponentsInChildren<WheelCollider>();

        if (carRB != null && carCenterOfMass != null)
        {
            carRB.centerOfMass = carCenterOfMass.localPosition;
        }

        // Set the motor torque ~0 to avoid wheel locking
        foreach (WheelCollider wheel in wheels)
        {
            wheel.motorTorque = 0f;
        }

        coroutine = RotateCar();
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

    }

    void FixedUpdate()
    {
        // Current speed
        currentSpeed = transform.InverseTransformDirection(carRB.velocity).z * speed;
       
        // Steering
        steering = turnInputCurve.Evaluate(inputManager.steeringDirection) * carStatsSO.steeringAngle;

        // Direction
        foreach (WheelCollider wheel in turnWheels)
        {
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steering, carStatsSO.steeringSpeed);
        }

        // Break
        Breake();

        // Accelerate and reverse
        Accelerate();

        // Ground Check
        if (!CheckGround() && Mathf.Abs(transform.localEulerAngles.z ) > 80.0f)
        {
            // Rotate Car
            StartCoroutine(RotateCar());
        }

        // Downforce
        carRB.AddForce(carStatsSO.extraGravity * currentSpeed * -transform.up);
    }

    public void Breake()
    {
        if (inputManager.isBreaking)
        {
            foreach (WheelCollider wheel in wheels)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = carStatsSO.brakingForce;
            }
        }
    }

    public void Accelerate()
    {
        if (!inputManager.isBreaking)
        {
            // Reset break torque
            foreach (WheelCollider wheel in wheels)
            {
                wheel.brakeTorque = 0;
            }

            foreach (WheelCollider wheel in rearWheels)
            {
                wheel.motorTorque = inputManager.accelerationInput * accelerationCurve.Evaluate(speed) * carStatsSO.diffGearing / rearWheels.Length;
            }
        }
    }

    public void OnDrift()
    {
        Debug.Log("Drifting: ");
    }

    // If car turn over, reset rotation to 0
    public IEnumerator RotateCar()
    {
        yield return new WaitForSeconds(4.0f);
        if (!CheckGround())
        {
            transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        }
        StopCoroutine(RotateCar());
    }

    public bool CheckGround()
    {
        int groundCount = 0;
        foreach(WheelCollider wheel in wheels)
        {
            if (wheel.isGrounded)
            {
                groundCount++;
            }
        }

        if (groundCount > 2)
        {
            return true;
        }
        else 
            return false;
    }
}
