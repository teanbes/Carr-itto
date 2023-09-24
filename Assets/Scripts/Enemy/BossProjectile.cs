using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private GameObject bossProjectile;
    [SerializeField] private Transform ProjectileSpawnPoint;

    public void SpawnBossProjectile()
    {
        Instantiate(bossProjectile, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
    }
}
