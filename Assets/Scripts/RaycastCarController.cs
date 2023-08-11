using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class RaycastCarController : MonoBehaviour
{
    [SerializeField] private Transform FrontRightWheel;
    [SerializeField] private Transform FrontLeftWheel;
    [SerializeField] private Transform RearRightWheel;
    [SerializeField] private Transform RearLeftWheel;

    [SerializeField] private Transform carTransform;
    [SerializeField] private float carTopSpeed = 20f;

    private RaycastHit RayFRWheel;
    private RaycastHit RayFLWheel;
    private RaycastHit RayRRWheel;
    private RaycastHit RayRLWheel;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private float suspensionRestDistance = 1f;
    [SerializeField] private float springDamper = 2f;
    [SerializeField] private float springStrength = 2f;

    private float steering;
    //Steering
    private float totalSteering;
    [SerializeField] private AnimationCurve turnInputCurve = AnimationCurve.Linear(-1.0f, -1.0f, 1.0f, 1.0f);

    [Range(0, 1)]
    [SerializeField] private float tireGripfactor = 0.3f;

    [SerializeField] private float tireMass = 20f;

    [SerializeField] private AnimationCurve powerCurve;

    [SerializeField] private InputManager inputManager;

    [SerializeField] private CarStatsSO carStatsSO;

    private float currentSteerAngle = 0.0f;
    public float rotationSpeed = 100.0f;
    public float maxSteerAngle = 30.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Steering
        // Get the current rotation
        Vector3 currentRotation = FrontRightWheel.localRotation.eulerAngles;

        // Calculate the new rotation based on the steering input
        float steerInput = inputManager.steeringDirection; // Assumes you're using the Horizontal axis for steering
        currentSteerAngle += steerInput * rotationSpeed * Time.deltaTime;

        // Clamp the steering angle to the defined maximum
        currentSteerAngle = Mathf.Clamp(currentSteerAngle, -maxSteerAngle, maxSteerAngle);

        // Apply the new rotation to the GameObject
        FrontRightWheel.localRotation = Quaternion.Euler(currentRotation.x, currentSteerAngle, currentRotation.z);


        if (Physics.Raycast(FrontRightWheel.transform.position, FrontRightWheel.transform.TransformDirection(Vector3.down), out RayFRWheel, rayDistance, layerMask))
        {
            Debug.DrawRay(FrontRightWheel.transform.position, FrontRightWheel.transform.TransformDirection(Vector3.down) * RayFRWheel.distance, Color.yellow);

            /// suspension
            /// 

            // World-space spring force direction
            Vector3 springDir = FrontRightWheel.up;

            //world-space tire velocity
            Vector3 tireWorldVel = carRB.GetPointVelocity(FrontRightWheel.position);

            // Offset from raycast
            float offset = suspensionRestDistance - RayFRWheel.distance;
            

            // Velocity along the spring direction
            float vel = Vector3.Dot(springDir, tireWorldVel);

            // Dampened spring force magnitude
            float force = (offset * springStrength) - (vel * springDamper);

            // Applyforce at tire location
            carRB.AddForceAtPosition(springDir * force, FrontRightWheel.position);

            /// Steering force
            ///
             
            Vector3 steeringDir = FrontRightWheel.right;

            // Tire vel in steering direction
            float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

            // desired change in velocity is -steeringVel * tireGripfactor;
            float desiredVelChange = -steeringVel * tireGripfactor;

            // turn change in vel into acceleration
            float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

            // Force = Mass * acceleration
            carRB.AddForceAtPosition(steeringDir * tireMass * desiredAccel, FrontRightWheel.position);

            /// acceleration
            ///
            // accel direction
            Vector3 accelDir = FrontRightWheel.forward;

            // accel torque
            if (inputManager.accelerationInput > 0.0f)
            {
                // forward speed of car 
                float carSpeed = Vector3.Dot(carTransform.forward, carRB.velocity);

                // normalized car speed
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

                // torque
                float availableTorque = powerCurve.Evaluate(normalizedSpeed) * inputManager.accelerationInput;

                carRB.AddForceAtPosition(accelDir * availableTorque, FrontRightWheel.position);
            }

        }

        if (Physics.Raycast(FrontLeftWheel.transform.position, FrontLeftWheel.transform.TransformDirection(Vector3.down), out RayFLWheel, rayDistance, layerMask))
        {
            Debug.DrawRay(FrontLeftWheel.transform.position, FrontLeftWheel.transform.TransformDirection(Vector3.down) * RayFLWheel.distance, Color.yellow);

            // World-space spring force direction
            Vector3 springDir = FrontLeftWheel.up;

            //world-space tire velocity
            Vector3 tireWorldVel = carRB.GetPointVelocity(FrontLeftWheel.position);

            // Offset from raycast
            float offset = suspensionRestDistance - RayFLWheel.distance;

            // Velocity along the spring direction
            float vel = Vector3.Dot(springDir, tireWorldVel);

            // Dampened spring force magnitude
            float force = (offset * springStrength) - (vel * springDamper);

            // Applyforce at tire location
            carRB.AddForceAtPosition(springDir * force, FrontLeftWheel.position);

            /// Steering force
            ///

            Vector3 steeringDir = FrontLeftWheel.right;
            // Tire vel in steering direction
            float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

            // desired change in velocity is -steeringVel * tireGripfactor;
            float desiredVelChange = -steeringVel * tireGripfactor;

            // turn change in vel into acceleration
            float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

            // Force = Mass * acceleration
            carRB.AddForceAtPosition(steeringDir * tireMass * desiredAccel, FrontLeftWheel.position);

            /// acceleration
            ///
            // accel direction
            Vector3 accelDir = FrontLeftWheel.forward;

            // accel torque
            if (inputManager.accelerationInput > 0.0f)
            {
                // forward speed of car 
                float carSpeed = Vector3.Dot(carTransform.forward, carRB.velocity);

                // normalized car speed
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

                // torque
                float availableTorque = powerCurve.Evaluate(normalizedSpeed) * inputManager.accelerationInput;

                carRB.AddForceAtPosition(accelDir * availableTorque, FrontLeftWheel.position);

            }
            }

        if (Physics.Raycast(RearRightWheel.transform.position, RearRightWheel.transform.TransformDirection(Vector3.down), out RayRRWheel, rayDistance, layerMask))
        {
            Debug.DrawRay(RearRightWheel.transform.position, RearRightWheel.transform.TransformDirection(Vector3.down) * RayRRWheel.distance, Color.yellow);

            // World-space spring force direction
            Vector3 springDir = RearRightWheel.up;

            //world-space tire velocity
            Vector3 tireWorldVel = carRB.GetPointVelocity(RearRightWheel.position);

            // Offset from raycast
            float offset = suspensionRestDistance - RayRRWheel.distance;

            // Velocity along the spring direction
            float vel = Vector3.Dot(springDir, tireWorldVel);

            // Dampened spring force magnitude
            float force = (offset * springStrength) - (vel * springDamper);

            // Applyforce at tire location
            carRB.AddForceAtPosition(springDir * force, RearRightWheel.position);

            /// Steering force
            ///

            Vector3 steeringDir = RearRightWheel.right;
            // Tire vel in steering direction
            float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

            // desired change in velocity is -steeringVel * tireGripfactor;
            float desiredVelChange = -steeringVel * tireGripfactor;

            // turn change in vel into acceleration
            float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

            // Force = Mass * acceleration
            carRB.AddForceAtPosition(steeringDir * tireMass * desiredAccel, RearRightWheel.position);

            /// acceleration
            ///
            // accel direction
            Vector3 accelDir = RearRightWheel.forward;

            // accel torque
            if (inputManager.accelerationInput > 0.0f)
            {
                // forward speed of car 
                float carSpeed = Vector3.Dot(carTransform.forward, carRB.velocity);

                // normalized car speed
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

                // torque
                float availableTorque = powerCurve.Evaluate(normalizedSpeed) * inputManager.accelerationInput;

                carRB.AddForceAtPosition(accelDir * availableTorque, RearRightWheel.position);

            }

        }

        if (Physics.Raycast(RearLeftWheel.transform.position, RearLeftWheel.transform.TransformDirection(Vector3.down), out RayRLWheel, rayDistance, layerMask))
        {
            Debug.DrawRay(RearLeftWheel.transform.position, RearLeftWheel.transform.TransformDirection(Vector3.down) * RayRLWheel.distance, Color.yellow);

            // World-space spring force direction
            Vector3 springDir = RearLeftWheel.up;

            //world-space tire velocity
            Vector3 tireWorldVel = carRB.GetPointVelocity(RearLeftWheel.position);

            // Offset from raycast
            float offset = suspensionRestDistance - RayRLWheel.distance;

            // Velocity along the spring direction
            float vel = Vector3.Dot(springDir, tireWorldVel);

            // Dampened spring force magnitude
            float force = (offset * springStrength) - (vel * springDamper);

            // Applyforce at tire location
            carRB.AddForceAtPosition(springDir * force, RearLeftWheel.position);

            /// Steering force
            ///

            Vector3 steeringDir = RearLeftWheel.right;
            // Tire vel in steering direction
            float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

            // desired change in velocity is -steeringVel * tireGripfactor;
            float desiredVelChange = -steeringVel * tireGripfactor;

            // turn change in vel into acceleration
            float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

            // Force = Mass * acceleration
            carRB.AddForceAtPosition(steeringDir * tireMass * desiredAccel, RearLeftWheel.position);

            /// acceleration
            ///
            // accel direction
            Vector3 accelDir = RearLeftWheel.forward;

            // accel torque
            if (inputManager.accelerationInput > 0.0f)
            {
                // forward speed of car 
                float carSpeed = Vector3.Dot(carTransform.forward, carRB.velocity);

                // normalized car speed
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

                // torque
                float availableTorque = powerCurve.Evaluate(normalizedSpeed) * inputManager.accelerationInput;

                carRB.AddForceAtPosition(accelDir * availableTorque, RearLeftWheel.position);

            }
        }


    }
}
