using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour, CarInputs.ICarActions
{
    private CarInputs CarInputs;
    public event Action DriftEvent;
    
    [HideInInspector] public bool isBreaking;
    [HideInInspector] public float accelerationInput;
    [HideInInspector] public float steeringDirection;
    [HideInInspector] public bool isShooting;


    private void Awake()
    {
        CarInputs = new CarInputs();
    }

    private void Start()
    {
        CarInputs.Car.SetCallbacks(this);// SetCallbacks calls the methods for us
        CarInputs.Car.Enable();
    }

    private void OnDestroy()
    {
        CarInputs.Car.Disable();
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            return;
        }
        float accel = context.action.ReadValue<float>();
        accelerationInput = accel;

    }
    public void OnSteering(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            return;
        }
        float steer = context.action.ReadValue<float>();
        steeringDirection = steer;

    }

    public void OnDrift(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            DriftEvent?.Invoke();
        }
    }

    public void OnBrake(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBreaking = true;
        }
        if (!context.performed)
        {
            isBreaking = false;
        }

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isShooting = true;
        }
        if (!context.performed)
        {
            isShooting = false;
        }
    }
}
