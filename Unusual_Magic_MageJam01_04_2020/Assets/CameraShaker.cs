using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;

    [SerializeField] float shakeDuration = 0.3f;
    [SerializeField] float shakeAmplitude = 1.2f;
    [SerializeField] float shakeFrequency = 2f;
    [SerializeField] CinemachineVirtualCamera vcam;

    private CinemachineBasicMultiChannelPerlin vcamNoise;
    private float shakeElapsedTime = 0f;

    private void Start()
    {
        instance = this;
        vcamNoise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if(shakeElapsedTime > 0)
        {
            vcamNoise.m_AmplitudeGain = shakeAmplitude;
            vcamNoise.m_FrequencyGain = shakeFrequency;

            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            vcamNoise.m_AmplitudeGain = 0f;
            shakeElapsedTime = 0f;
        }
    }


    public void Shake()
    {
        shakeElapsedTime = shakeDuration;
    }
}
