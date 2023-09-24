using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostInSpace : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<CarController>())
        {
            Debug.Log("You Are Lost In Space");
            Time.timeScale = 0.0f;
        }
    }


}
