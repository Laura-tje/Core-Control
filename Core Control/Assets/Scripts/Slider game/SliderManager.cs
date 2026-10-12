using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour, IGameInteractable
{
    public static SliderManager Instance { get; private set; }

    public int slidersBroken;

    [SerializeField] private float minBreakInterval;
    [SerializeField] private float maxBreakInterval;
    [SerializeField] private PressureGauge pressureGauge;
    [SerializeField] private float severity;

    private Slide[] sliders;
    private Coroutine breakRoutine;

    // true zodra de gauge deze game kapot heeft gemaakt en hij nog niet is opgelost
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
        if (pressureGauge == null)
        {
            pressureGauge = FindAnyObjectByType<PressureGauge>();
        }

        // De game begint heel; pas ActivateGame() maakt hem kapot
        minigameActive = false;
    }

    // IGameInteractable: elke slider krijgt een nieuw willekeurig doel
    public void ActivateGame()
    {
        minigameActive = true;

        foreach (Slide slide in sliders)
            slide.Break();

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

        if (!minigameActive) return;   // niet kapot gemaakt, dus niks op te lossen

        minigameActive = false;

        if (pressureGauge != null)
            pressureGauge.MiniGameSolved((int)PressureGauge.EGames.SliderGame);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}