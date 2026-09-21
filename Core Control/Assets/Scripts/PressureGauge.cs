using System;
using UnityEngine;
using Unity.Mathematics;

public class PressureGauge : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private GameObject gauge;
    [SerializeField] private float gaugeRotation;
    public GameObject[] features; //put the switches and stuff in here
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 100;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }

    private int lookForNewProblem() //returned number from features array om nieuw probleem te zoeken, trust the process
    {
        return UnityEngine.Random.Range(0,features.Length);
    }
}
