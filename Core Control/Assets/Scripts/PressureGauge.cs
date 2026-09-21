using System;
using UnityEngine;
using Unity.Mathematics;

public class PressureGauge : MonoBehaviour
{
    [SerializeField] private float timer; //0-50 is groen, 50-80 is oranje, 80-100 is rood
    [SerializeField] private GameObject gauge;
    [SerializeField] private float gaugeRotation;
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

        if (timer >= 100)
        {
            Explode();
        }
        else
        {
            timer += Time.deltaTime;
        }
        
        gauge.transform.rotation = Quaternion.Euler(0, 0, (gaugeRotation + 90));
    }

    private int lookForNewProblem() //returned number from features array om nieuw probleem te zoeken, trust the process
    {
        return UnityEngine.Random.Range(0,features.Length);
    }

    private void Explode()
    {
        Debug.Log("Explode, you died :( !");
    }
}
