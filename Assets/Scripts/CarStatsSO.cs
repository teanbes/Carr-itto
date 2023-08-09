using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarStats", menuName = "ScriptableObjects/CarVariables", order = 1)]
public class CarStatsSO : ScriptableObject
{
    [Min(0.1f), Tooltip("Max Speed")]
    public float topSpeed;

    [Tooltip("Acceleration")]
    public float acceleration;

    [Min(0.1f), Tooltip("Reverse Speed")]
    public float reverseSpeed;

    [Tooltip("Reverse Acceleration")]
    public float reverseAcceleration;

    [Tooltip("Acceleration Curve")]
    [Range(0.2f, 1)]
    public float accelerationCurve;

    [Tooltip("Braking")]
    public float braking;

    [Tooltip("Drag")]
    public float drag;

    [Range(0.0f, 1.0f)]
    [Tooltip("Grip Friction")]
    public float grip;

    [Tooltip("Steering")]
    public float steering;

    [Tooltip("Gravity")]
    public float extraGravity;
}
