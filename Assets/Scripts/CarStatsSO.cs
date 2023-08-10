using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarStats", menuName = "ScriptableObjects/CarVariables", order = 1)]
public class CarStatsSO : ScriptableObject
{
    
    [Tooltip("Acceleration")]
    [Range(-1.0f, 1.0f)]
    public float acceleration;

    [Min(0.1f), Tooltip("Reverse MaxSpeed")]
    public float reverseSpeed;
    
    [Tooltip("Braking Force")]
    [Range(1000, 3000)]
    public float brakingForce = 1500.0f;

    [Tooltip("Differential Gearing Ratio")]
    [Range(2, 16)]
    public float diffGearing = 4.0f;

    [Tooltip("Drift Amount")]
    [Range(0.0f, 2f)]
    public float drift = 1.0f;

    [Tooltip("Steering Speed Ratio")]
    [Range(0.001f, 1.0f)]
    public float steeringSpeed = 0.2f;

    [Tooltip("Steering Speed")]
    [Range(0.0f, 50.0f)]
    public float steeringAngle = 30.0f;


    [Tooltip("Extra Down Force Gravity")]
    [Range(0.0f, 5.0f)]
    public float extraGravity = 1f;
}
