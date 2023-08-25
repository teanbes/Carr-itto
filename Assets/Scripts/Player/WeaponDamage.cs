using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float speed = 30f;
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;
                 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            Destroy(this.gameObject);
        }
    }

}
