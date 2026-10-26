using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class RadarGame : MonoBehaviour, IGameInteractable
{
    [SerializeField] bool isRadarActive = false;
    public GameObject ChosenButton;
    [SerializeField] GameObject[] Buttons;

    public int score = 0;
    public int scoreToWin = 5;

    [SerializeField] private Image LEDIndicator;

    void Start()
    {
        Buttons = GameObject.FindGameObjectsWithTag("RadarButtons");
        foreach (var button in Buttons)
        {
            button.SetActive(false);
        }

        
    }

    private void Update()
    {
        if (score >= scoreToWin)
        {
            isRadarActive = false;
            PressureGauge.Instance.MiniGameSolved((int)PressureGauge.EGames.RadarGame);
            ChangeIndicator(PressureGauge.Instance.onLED);
        }

    }


    public void ActivateButton()
    {
        
        GameObject ChosenButtonTEMP;

        while (true)
        {
            ChosenButtonTEMP = Buttons[UnityEngine.Random.Range(0, Buttons.Length)];
            if (ChosenButtonTEMP != ChosenButton)
            {
                break;
            }
        }
            
        ChosenButton = ChosenButtonTEMP;
        ChosenButton.SetActive(true);
        
    }

    public void ActivateGame()
    {
        score = 0;
        isRadarActive = true;
        ActivateButton();
    }

    public void ChangeIndicator(Sprite indicator)
    {
        LEDIndicator.sprite = indicator;
    }
}
