using System;
using UnityEngine;
using Unity.Mathematics;

public class PressureGauge : MonoBehaviour
{
    [SerializeField] private float timer; //0-50 is groen, 50-80 is oranje, 80-100 is rood
    [SerializeField] private GameObject gauge;
    [SerializeField] private float gaugeRotation;
    public float timeIncrease = 1; //als iets onstabiel is, add 1 of 2 hieraan om de timer sneller te laten gaan, zodra gefixed, haal het er weer af.
    public GameObject[] features; //put the switches and stuff in here
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0; //0-50 is groen, 50-80 is oranje, 80-100 is rood
    }

    // Update is called once per frame
    void Update()
    {
        gaugeRotation = -timer * 1.8f;
        gauge.transform.rotation = Quaternion.Euler(0, 0, (gaugeRotation + 90));
        
        if (timeIncrease <= 0)
        {
            timeIncrease = 1;
        }
        
        if (timer >= 100)
        {
            Explode();
        }
        else
        {
            timer += timeIncrease * Time.deltaTime;
        }
    }

    private void Explode()
    {
        Debug.Log("Explode, you died :( !");
    }
}
