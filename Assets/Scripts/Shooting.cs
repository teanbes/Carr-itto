using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectileSpawnPoint1;
    [SerializeField] private GameObject projectileSpawnPoint2;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float fireRate = 2.0f;
    private float lastBulletFiredTime;


    // Start is called before the first frame update
    void Start()
    {
        lastBulletFiredTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        lastBulletFiredTime += Time.deltaTime;

        if (inputManager.isShooting && lastBulletFiredTime >= fireRate)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint1.transform.position, projectileSpawnPoint1.transform.rotation);
            Instantiate(projectilePrefab, projectileSpawnPoint2.transform.position, projectileSpawnPoint2.transform.rotation);
            lastBulletFiredTime = 0.0f;
        }
    }
}
