using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;


public class TurretFollowProjectile : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float initialSpeed = 7f;
    [SerializeField] private float followSpeed = 7f;
    [SerializeField] private float stopFollowDistance = 8f;
    [SerializeField] private float initialHeight = 30f;
    [SerializeField] private float midAirDelayTime = 1.5f;
    [SerializeField] private GameObject hit;
    [SerializeField] private GameObject shockWave;
    [SerializeField] private int damage = 8;

    private Rigidbody rb;
    private bool isFollowing = false;
    private float  projectileImpactForce = 100;
    private bool hasSpawnedShockWave = false;

   

    private void Start()
    {
        if (GameManager.instance.playerIsDead) { return; }
            
        player = GameObject.FindObjectOfType<CarController>().transform;
        rb = GetComponent<Rigidbody>();

        // move projectile to position
        rb.velocity = transform.up * initialSpeed;
    }

    private void Update()
    {
        if (GameManager.instance.playerIsDead) { return; }  
        
        if (!isFollowing && transform.position.y >= initialHeight)
        {
            // Prepare projectile to follow player
            rb.velocity = Vector3.zero;
            StartCoroutine(MidAirDelay());
        }

        if (isFollowing && player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer <= stopFollowDistance)
            {
                // Stop following player and continue with momentum direction
                isFollowing = false;
            }
            else
            {
                // Follow the player
                directionToPlayer.Normalize();
                rb.velocity = directionToPlayer * followSpeed;
            }
        }
    }

   


    private IEnumerator MidAirDelay()
    {

        yield return new WaitForSeconds(midAirDelayTime);
        // Spawn shockWave particles 
        if (!hasSpawnedShockWave)
        {
            Instantiate(shockWave, transform.position, transform.rotation);
            hasSpawnedShockWave = true;
        }
        // Start following player
        isFollowing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<CarController>())
        {
            // Apply Impact Force
            Vector3 directionToPlayer = player.position - transform.position;
            other.GetComponent<Rigidbody>().AddForce(directionToPlayer * projectileImpactForce, ForceMode.Acceleration);
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            if (other.TryGetComponent<Health>(out Health health))
            {
                health.DealDamage(damage);
                CinemachineShake.Instance.ShakeCamera(1f, 0.1f);
            }
        }
        // Spawn hit particles
        Instantiate(hit, transform.position, transform.rotation);
        Destroy(gameObject);
    }
     
}
