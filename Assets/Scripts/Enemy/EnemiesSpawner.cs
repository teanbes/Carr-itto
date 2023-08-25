using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesMaxAmount = 20;
    [SerializeField] private Transform[] spawnPoints;
    [HideInInspector] public int currentEnemiesAmount;


    // Start is called before the first frame update
    void Start()
    {
        currentEnemiesAmount = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            currentEnemiesAmount++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentEnemiesAmount);

        if (currentEnemiesAmount <= enemiesMaxAmount)
        {
            int index = Random.Range(0, spawnPoints.Length);
            int extraEnemies = Mathf.Clamp( enemiesMaxAmount - currentEnemiesAmount, 0, enemiesMaxAmount);
           
            Instantiate(enemyPrefab, spawnPoints[index].position, spawnPoints[index].rotation);
            currentEnemiesAmount++;

        } 
    }
}
