using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressureGauge : MonoBehaviour
{
    public static PressureGauge Instance { get; private set; }

    [SerializeField] private float timer; // 0-50 is groen, 50-80 is oranje, 80-100 is rood
    [SerializeField] private GameObject gauge;
    [SerializeField] private float gaugeRotation;

    [SerializeField] private float gaugeMin;
    [SerializeField] private float gaugeMax;

    // aantal actieve problemen, hoe hoger hoe sneller de timer loopt
    public int timeIncrease = 0;

    public GameObject[] features; // put the switches and stuff in here

    [SerializeField] private GameObject BrokenGlass;

    public List<bool> IsProblem;
    public List<GameObject> Games;
    [SerializeField] private int maxAmountOfProblems;
    [SerializeField] private float problemSpawnDelay = 3f;

    private float startingRotation;
    private float spawnTimer;
    private bool isExploded;

    public enum EGames
    {
        CableGame = 0,
        NumpadGame = 1,
        DraaiknopGame = 2,
        SliderGame = 3,
        FingerprintGame = 4,
        ButtonGame = 5,
        RadarGame = 6,
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void Start()
    {
        timer = 0;
        startingRotation = gauge.transform.localEulerAngles.z;
        BrokenGlass.SetActive(false);
    }

    private void Update()
    {
        if (isExploded) return;

        UpdateTimer();
        UpdateGaugeVisual();

        if (timer >= 100)
        {
            isExploded = true;
            StartCoroutine(Explode());
            return;
        }

        HandleProblemSpawning();
    }

    private void UpdateTimer()
    {
        timeIncrease = CheckForAmountProblems();

        if (timeIncrease <= 0)
            timer -= Time.deltaTime;
        else
            timer += timeIncrease * Time.deltaTime;

        timer = Mathf.Max(timer, 0f);
    }

    private void UpdateGaugeVisual()
    {
        gaugeRotation = Mathf.Lerp(gaugeMin, gaugeMax, timer / 100f);
        gauge.transform.localRotation = Quaternion.Euler(0, 0, startingRotation + gaugeRotation);
    }

    private void HandleProblemSpawning()
    {
        if (CheckForAmountProblems() >= maxAmountOfProblems)
        {
            spawnTimer = 0;
            return;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer < problemSpawnDelay) return;

        spawnTimer = 0;

        int index = FindNewProblem();
        if (index < 0) return; // alles is al een probleem

        IsProblem[index] = true;

        // maar 1 keer activeren, op het moment dat het probleem ontstaat
        IGameInteractable minigame = Games[index].GetComponent<IGameInteractable>();
        if (minigame != null)
        {
            minigame.ActivateGame();
        }
    }

    public void MiniGameSolved(int whichGame)
    {
        if (whichGame < 0 || whichGame >= IsProblem.Count) return;
        IsProblem[whichGame] = false;
    }

    private IEnumerator Explode()
    {
        Debug.Log("Explode, you died :( !");
        BrokenGlass.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // geeft -1 terug als er geen vrije game meer is
    private int FindNewProblem()
    {
        List<int> free = new List<int>();

        int count = Mathf.Min(IsProblem.Count, Games.Count);
        for (int i = 0; i < count; i++)
        {
            if (!IsProblem[i]) free.Add(i);
        }

        if (free.Count == 0) return -1;
        return free[Random.Range(0, free.Count)];
    }

    private int CheckForAmountProblems()
    {
        int counter = 0;
        for (int i = 0; i < IsProblem.Count; i++)
        {
            if (IsProblem[i]) counter++;
        }
        return counter;
    }
}