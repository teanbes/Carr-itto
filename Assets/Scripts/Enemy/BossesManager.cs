using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bosses;

 
    void Update()
    {
        ActivateBoss();
    }

    private void ActivateBoss()
    {
        int enemiesDestroyed = GameManager.instance.enemiesDestroyed;
        int bossesDestroyed = GameManager.instance.boosesDestroyed;

        if (enemiesDestroyed >= 15 && enemiesDestroyed <= 44 && bossesDestroyed == 0 )
        {
            bosses[0].SetActive(true);
            AudioManager.Instance.Play("BossSpawn");
        }
        else if (enemiesDestroyed >= 45 && enemiesDestroyed <= 79 && bossesDestroyed == 1)
        {
            bosses[1].SetActive(true);
            AudioManager.Instance.Play("BossSpawn");
        }
        else if (enemiesDestroyed >= 80 && enemiesDestroyed <= 150 && bossesDestroyed == 2)
        {
            bosses[0].SetActive(true);
            bosses[1].SetActive(true);
            AudioManager.Instance.Play("BossSpawn");
        }
        else if (enemiesDestroyed >= 150 && enemiesDestroyed <= 240 && bossesDestroyed == 4)
        {
            bosses[0].SetActive(true);
            AudioManager.Instance.Play("BossSpawn");
            bosses[1].SetActive(true);
            AudioManager.Instance.Play("BossSpawn");
            bosses[2].SetActive(true);
        }
        else if (enemiesDestroyed >= 240 && enemiesDestroyed <= 300 && bossesDestroyed == 7)
        {
            bosses[0].SetActive(true);
            AudioManager.Instance.Play("BossSpawn");
            bosses[1].SetActive(true);
            AudioManager.Instance.Play("BossSpawn");
            bosses[2].SetActive(true);
        }
    }
}
