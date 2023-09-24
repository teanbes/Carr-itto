using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostInSpace : MonoBehaviour
{
    [SerializeField] private GameObject lostInSpacePanel;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<CarController>())
        {
            lostInSpacePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }


}
