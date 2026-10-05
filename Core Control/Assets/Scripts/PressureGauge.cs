using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using System.Collections;
using System.Collections.Generic;


public class PressureGauge : MonoBehaviour
{
    public static PressureGauge Instance { get; set; }

    [SerializeField] private float timer; //0-50 is groen, 50-80 is oranje, 80-100 is rood
    [SerializeField] private GameObject gauge;
    [SerializeField] private float gaugeRotation;

    [SerializeField] private float gaugeMin;
    [SerializeField] private float gaugeMax;

    public int timeIncrease = 0; //als iets onstabiel is, add 1 of 2 hieraan om de timer sneller te laten gaan, zodra gefixed, haal het er weer af.
    public GameObject[] features; //put the switches and stuff in here

    [SerializeField] private GameObject BrokenGlass;

    private float startingRotation = 145f;

    public List<bool> IsProblem;
    [SerializeField] private int maxAmountOfProblems;
    
    [SerializeField] private float problemSpawnDelay = 3f;
    private float spawnTimer;
    private bool isExploded;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject); // Haal de // weg als de gauge over scenes heen moet blijven bestaan
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        timer = 0;

        startingRotation = gauge.transform.localEulerAngles.z;

        BrokenGlass.SetActive(false);
    }

    void Update()
    {
        if (isExploded) return;
        
        gaugeRotation = Mathf.Lerp(gaugeMin, gaugeMax, timer / 100f);
        gauge.transform.localRotation = Quaternion.Euler(0, 0, startingRotation + gaugeRotation);

        if (timeIncrease <= 0)
        {
            timeIncrease = 0;
            timer -= Time.deltaTime;

        }
        else
        {
            timer += timeIncrease * Time.deltaTime;
        }

        if (timer >= 100)
        {
            isExploded = true;
            StartCoroutine(Explode());
            return;
        }
        if (timer < 0) timer = 0;

        if (CheckForAmountProblems() < maxAmountOfProblems)
        {
            IsProblem[FindNewProblem()] = true;
        }
        
        timeIncrease = CheckForAmountProblems();
        
    }

    private IEnumerator Explode()
    {
        Debug.Log("Explode, you died :( !");
        BrokenGlass.SetActive(true); 
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    private int FindNewProblem()
    {
        List<int> falseProblems = new List<int>();

        for (int i = 0; i < IsProblem.Count; i++)
        {
            if (!IsProblem[i])
                falseProblems.Add(i);
        }

        return falseProblems[UnityEngine.Random.Range(0, falseProblems.Count)];
    }

    private int CheckForAmountProblems()
    {
        int counter = 0;

        for (int i = 0; i < IsProblem.Count; i++)
        {
            if (IsProblem[i])
            {
                counter++;
            }
        }
        return counter;
    }
}
