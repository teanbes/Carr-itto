using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject bossReference;
    [SerializeField] private HealthBar healthBar;

    [Header("Health")]
    [SerializeField] private Health bossHealth;
    [SerializeField] private bool bossDead;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private Transform hitSpawnPoint;



    private void Update()
    {
        if (bossDead)
        {
            
            bossHealth.health = 2000;
            bossDead = false;
        }

    }

    private void OnEnable()
    {
        bossHealth.OnTakeDamage += HandleTakeDamage;
        bossHealth.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        bossHealth.OnTakeDamage -= HandleTakeDamage;
        bossHealth.OnDie -= HandleDie;
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WeaponDamage>())
        {
            HandleTakeDamage();
        }


    }

    private void HandleTakeDamage()
    {
        Debug.Log("doing damage");
        Instantiate(hitParticle, hitSpawnPoint.position, Quaternion.identity);
        healthBar.UpdateHealthBar(bossHealth.health);
    }

    private void HandleDie()
    {
        bossDead = true;
        CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        AudioManager.Instance.Play("BossDeath");
        AudioManager.Instance.Play("Explosion");
        bossReference.SetActive(false);
        GameManager.instance.enemiesDestroyed +=20; 

    }
}
