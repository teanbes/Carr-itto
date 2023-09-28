using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bosses;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActivateBoss();
    }

    private void ActivateBoss()
    {
        switch (GameManager.instance.enemiesDestroyed)
        {
            case 15:
                bosses[0].SetActive(true);
                AudioManager.Instance.Play("BossSpawn");
            break;

            case 45:
                bosses[1].SetActive(true);
                AudioManager.Instance.Play("BossSpawn");
                break;

            case 80:
                bosses[0].SetActive(true);
                bosses[1].SetActive(true);
                AudioManager.Instance.Play("BossSpawn");
                break;

            case 120:
                bosses[0].SetActive(true);
                AudioManager.Instance.Play("BossSpawn");
                bosses[1].SetActive(true);
                AudioManager.Instance.Play("BossSpawn");
                bosses[2].SetActive(true);
            break;
        }
    }
}
