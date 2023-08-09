using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private CarStatsSO carStatsSO;
    [SerializeField] private GameObject car;
    [SerializeField] private Transform carCenterOfMass;

    [SerializeField] private InputManager inputManager;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void OnAccelerate()
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Aceleracion: " + inputManager.accelerationInput);
        
    }


}
