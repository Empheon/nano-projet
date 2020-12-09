using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float m_traumaDecreaseSpeed = 1.0f;

    [SerializeField]
    private float m_traumaPower = 2.0f;

    [SerializeField]
    private float m_frequency = 0.3f;

    [SerializeField]
    private float m_maxPitch = 1.0f;

    [SerializeField]
    private float m_maxYaw = 1.0f;

    [SerializeField]
    private float m_maxRoll = 1.0f;

    [SerializeField]
    private float m_maxTrauma = 1.0f;


    private float m_trauma;
    private FastNoise m_noise;

    public static CameraShake Instance;

    public void Start()
    {
        Instance = this;

        InitNoise();
    }

    private void InitNoise()
    {
        m_noise = new FastNoise(Random.Range(0, 10000));
        m_noise.SetNoiseType(FastNoise.NoiseType.PerlinFractal);
        m_noise.SetFrequency(m_frequency);
        m_noise.SetFractalOctaves(3);
        m_noise.SetFractalGain(0.5f);
    }

    public void LateUpdate()
    {
        if(m_noise == null)
        {
            InitNoise();
        }

        float shakeAmount = Mathf.Pow(m_trauma, m_traumaPower);

        float newPitch = m_maxPitch * shakeAmount * m_noise.GetValue(0, Time.time * 100.0f);
        float newYaw = m_maxYaw * shakeAmount * m_noise.GetValue(100, Time.time * 100.0f);
        float newRoll = m_maxRoll * shakeAmount * m_noise.GetValue(200, Time.time * 100.0f);
        transform.localRotation = Quaternion.Euler(newPitch, newYaw, newRoll);

        m_trauma = Mathf.Max(m_trauma - Time.deltaTime * m_traumaDecreaseSpeed, 0.0f);
    }

    public void AddTrauma(float trauma)
    {
        m_trauma = Mathf.Clamp(m_trauma + trauma, 0.0f, m_maxTrauma);
    }
}
