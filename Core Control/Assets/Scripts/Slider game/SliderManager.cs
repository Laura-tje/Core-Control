using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    public static SliderManager Instance { get; private set; }

    public int slidersBroken;

    [SerializeField] private float minBreakInterval;
    [SerializeField] private float maxBreakInterval;
    [SerializeField] private PressureGauge pressureGauge;
    [SerializeField] private float severity;

    private Slide[] sliders;
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
        sliders = GetComponentsInChildren<Slide>();
    }

    private void Start()
    {
        breakRoutine = StartCoroutine(BreakRoutine());

        if (pressureGauge == null)
        {
            pressureGauge = FindAnyObjectByType<PressureGauge>();
        }

        minigameActive = true;
        pressureGauge.timeIncrease += severity;
    }

    private IEnumerator BreakRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minBreakInterval, maxBreakInterval));
            BreakRandomSlider();
        }
    }

    private void BreakRandomSlider()
    {
        List<Slide> correct = new List<Slide>();
        foreach (Slide slide in sliders)
        {
            if (slide.isCorrect)
                correct.Add(slide);
        }

        if (correct.Count == 0) return;

        Slide victim = correct[Random.Range(0, correct.Count)];
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
        slidersBroken = 0;
        foreach (Slide slide in sliders)
        {
            if (slide.isWrong)
                slidersBroken++;
        }
    }

    public void SliderConnected(Slide slide)
    {
        UpdateBrokenCount();

        CheckAllCorrect();
    }

    private void CheckAllCorrect()
    {
        foreach (Slide slide in sliders)
        {
            if (!slide.isCorrect)
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