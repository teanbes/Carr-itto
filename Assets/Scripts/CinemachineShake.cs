using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    private void Start()
    {
        StartCoroutine(InitializeCinemachineCamera());
    }

    private IEnumerator InitializeCinemachineCamera()
    {
        // Wait for a short delay (adjust the time as needed)
        yield return new WaitForSeconds(0.5f);

        // Attempt to get the CinemachineVirtualCamera component
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (cinemachineVirtualCamera != null)
        {
            Debug.Log("CinemachineVirtualCamera found.");
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera not found.");
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        if (cinemachineVirtualCamera != null)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (cinemachineBasicMultiChannelPerlin != null)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
                shakeTimer = time;
            }
            else
            {
                Debug.LogError("CinemachineBasicMultiChannelPerlin component not found on the Cinemachine Virtual Camera.");
            }
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera not assigned.");
        }
    }

    private void Update() 
    {
        
        if (shakeTimer > 0) 
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                // timer over
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f ;

            }

        }
    }

}
