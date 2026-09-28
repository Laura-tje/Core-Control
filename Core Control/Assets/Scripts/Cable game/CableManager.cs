using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public static CableManager Instance { get; private set; }

    public int cablesBroken;

    [SerializeField] private float minBreakInterval = 3f;
    [SerializeField] private float maxBreakInterval = 8f;
    [SerializeField] private PressureGauge pressureGauge;
    [SerializeField] private float severity;

    private Cable[] cables;
    private readonly List<Cable> brokenCables = new List<Cable>();
    private Coroutine breakRoutine;
    private bool minigameActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        cables = GetComponentsInChildren<Cable>();
        breakRoutine = StartCoroutine(BreakRoutine());
    }

    private IEnumerator BreakRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minBreakInterval, maxBreakInterval));
            BreakRandomCable();
        }
    }

    private void BreakRandomCable()
    {
        List<Cable> connected = new List<Cable>();
        foreach (Cable cable in cables)
        {
            if (cable.isConnected)
            connected.Add(cable);
        }

        if (connected.Count == 0) return;

        Cable victim = connected[Random.Range(0, connected.Count)];
        victim.Break();

        if (!minigameActive)
        {
            minigameActive = true;
            pressureGauge.timeIncrease += severity;
        }

        UpdateBrokenCount();
    }

    private void UpdateBrokenCount()
    {
        cablesBroken = 0;
        foreach (Cable cable in cables)
        {
            if (cable.isBroken)
                cablesBroken++;
        }
    }

    public void CableConnected(Cable cable)
    {
        UpdateBrokenCount();

        CheckAllConnected();
    }

    private void CheckAllConnected()
    {
        foreach (Cable cable in cables)
        {
            if (!cable.isConnected)
                return;
        }

        if (minigameActive)
        {
            minigameActive = false;
            pressureGauge.timeIncrease -= severity;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}