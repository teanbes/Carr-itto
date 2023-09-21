using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostInSpace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.GetComponent<CarController>())
        {
            Debug.Log("You Are Lost In Space");
        }
    }


}
