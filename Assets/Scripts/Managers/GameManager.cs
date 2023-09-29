using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;

    public static GameManager instance
    {
        get => _instance;
    }

    public CarController carPrefab;
    [HideInInspector] public Transform currentSpawnPoint;

    [HideInInspector] public String scoreText;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public float score;
    private float speed;
    [HideInInspector] public int enemiesDestroyed;
    [HideInInspector] public int boosesDestroyed;
    [HideInInspector] public float speedInstance;
    [HideInInspector] public bool playerIsDead = false;
    [HideInInspector] public float timer;
    [HideInInspector] public String timerText;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (!carPrefab)
            carPrefab = GameObject.FindObjectOfType<CarController>();
    }

    private void Start()
    {
        enemiesDestroyed = 0;
        boosesDestroyed = 0;
        score = 0;
        timer = 0;
    }
   

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            score = enemiesDestroyed;
            scoreText = score.ToString();

            if (!playerIsDead) 
            {
                timer += Time.deltaTime;
                timerText = timer.ToString();
            }
            
        }
    }



}
