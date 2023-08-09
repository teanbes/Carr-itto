using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, CarInputs.ICarActions
{
    private CarInputs CarInputs;
    public event Action DriftEvent;
    public float accelerationInput;
    public float steeringDirection;


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

    public void OnMove(InputAction.CallbackContext context)
    {
       
    }

   
}
