using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CarStatsSO carStatsSO;
    [SerializeField] private GameObject car;
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform carCenterOfMass;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject stopLights;
    
    [Header("Aditional Car Stats")]
    [SerializeField] private const float maxSpeed = 200;
    [SerializeField] private AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0, maxSpeed), new Keyframe(maxSpeed, 0));
    [SerializeField] private AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);
    [SerializeField] private float speed = 3.6f;
    private float steering;
    [HideInInspector] public float currentSpeed;
  
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
    [SerializeField] private GameObject[] wheelPrefabs;
    private WheelCollider[] wheelCollider;

    [Header("Health")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private bool playerDead;
    [SerializeField] private GameObject[] damageParticles;
    private bool isDead = false;

    private float speedToUI;

    // Start is called before the first frame update
    private void Start()
    {
        carRB = GetComponent<Rigidbody>();
        wheelCollider = GetComponentsInChildren<WheelCollider>();

        if (carRB != null && carCenterOfMass != null)
        {
            carRB.centerOfMass = carCenterOfMass.localPosition;
        }

        foreach (WheelCollider wheel in wheelCollider)
        {
            wheel.motorTorque = 0f;
        }
    }
       
    private void OnEnable()
    {
        inputManager.DriftEvent += OnDrift;
        playerHealth.OnTakeDamage += HandleTakeDamage;
        playerHealth.OnDie += HandleDie;
    }


    private void OnDisable()
    {
        inputManager.DriftEvent -= OnDrift;
        playerHealth.OnTakeDamage -= HandleTakeDamage;
        playerHealth.OnDie -= HandleDie;
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

        // Wheels Animation
        WheelsAnimation();

        // Ground Check
        if (!GroundCheck() && Mathf.Abs(transform.localEulerAngles.z ) > 80.0f)
        {
            // Rotate Car
            StartCoroutine(RotateCar());
        }
  
        // Downforce
        carRB.AddForce(carStatsSO.extraGravity * currentSpeed * -transform.up);

        if (Mathf.Abs(currentSpeed) > 1)
            speedToUI = Mathf.Clamp(Mathf.Abs(currentSpeed), 0, 200);
        else speedToUI = 0;
        GameManager.instance.currentSpeed = currentSpeed;
    }

    public void Breake()
    {
        if (inputManager.isBreaking)
        {
            stopLights.SetActive(true);
            foreach (WheelCollider wheel in wheelCollider)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = carStatsSO.brakingForce;
            }
        }
        else
        {
            stopLights.SetActive(false);
        }
    }

    public void Accelerate()
    {
        if (!inputManager.isBreaking)
        {
            // Reset break torque
            foreach (WheelCollider wheel in wheelCollider)
            {
                wheel.brakeTorque = 0;
            }

            foreach (WheelCollider wheel in rearWheels)
            {
                wheel.motorTorque = inputManager.accelerationInput * accelerationCurve.Evaluate(currentSpeed) * carStatsSO.diffGearing / rearWheels.Length;
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
        yield return new WaitForSeconds(6.0f);
        if (!GroundCheck())
        {
            transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        }
        StopCoroutine(RotateCar());
    }

    // Ground Check
    public bool GroundCheck()
    {
        int groundCount = 0;
        foreach(WheelCollider wheel in wheelCollider)
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

    // Wheels Animation
    private void WheelsAnimation()
    {
        for (int i = 0; i < wheelCollider.Length; i++)
        {
            Vector3 pos = new Vector3(0, 0, 0);
            Quaternion quat = new Quaternion();
            wheelCollider[i].GetWorldPose(out pos, out quat);

            // Update Wheel Prefabs rotation and position
            wheelPrefabs[i].transform.rotation = quat;
            wheelPrefabs[i].transform.position = pos;
        }
    }

    private void HandleTakeDamage()
    {
        Debug.Log("Hola)");

        if ( playerHealth.health >= 80 && playerHealth.health <= 85)
        {
            damageParticles[0].SetActive(true);
        }
        else if (playerHealth.health >= 70 && playerHealth.health <= 71)
        { 
            damageParticles[1].SetActive(true); 
        }

        else if (playerHealth.health >= 50 && playerHealth.health <= 51)
        { 
            damageParticles[2].SetActive(true); 
        }
        else if (playerHealth.health >= 40 && playerHealth.health <= 42)
        { 
            damageParticles[3].SetActive(true); 
        }
        else if (playerHealth.health >= 0 && playerHealth.health <= 20)
        { 
            damageParticles[4].SetActive(true);
            damageParticles[5].SetActive(true);
        }


        //animator.CrossFadeInFixedTime(GetHitHash, CrossFadeDuration);
        //StartCoroutine(AnimationDelay());
    }

    private void HandleDie()
    {
        isDead = true;
        
       // Instantiate(deathParticles, transform.position, Quaternion.identity);
       // Destroy(gameObject);

    }
}
