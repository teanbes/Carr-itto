using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float speed = 30f;
    [SerializeField] GameObject projectileParent;
    private Rigidbody rb;
    private float projectileLife = 3f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioManager.Instance.Play("Shoot");

        if (projectileLife <= 0)
            projectileLife = 2f;

        Destroy(projectileParent, projectileLife);
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
            AudioManager.Instance.Play("EnemyDead");
        }
        Destroy(projectileParent);

    }

}
