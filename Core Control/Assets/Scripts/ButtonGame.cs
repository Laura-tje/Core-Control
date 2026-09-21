using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonGame : MonoBehaviour
{
    public List<ButtonHandler> Buttons;

    [SerializeField] private ButtonHandler ButtonChosen;
    private int PrevNum = -1;

    [SerializeField] private float TimeToWait = 60f;
    [SerializeField] private float TimePassed = 0f;
    [SerializeField] private int ButtonsPressed = 0;
    [SerializeField] private int ButtonsToPress = 10;

    [SerializeField] private bool GameStart = false;
    
    [SerializeField] PressureGauge PressureGauge;
    [SerializeField] private float Severity = 2f;

    private void Start()
    {
        Debug.Log("ButtonGame started with " + Buttons.Count + " buttons.");
    }

    private void Update()
    {
        
        TimePassed += Time.deltaTime;

        if(GameStart == true)
        {
            if (ButtonsPressed == ButtonsToPress)
            {
                Debug.Log("You win!");
                GameStart = false;
                TimePassed = 0f;
                TimeToWait = Random.Range(30f, 120f);
                PressureGauge.timeIncrease -= Severity;
            }
            else
            {
                ButtonGamePlay();
            }
        } 
        if(GameStart == false && TimePassed >= TimeToWait)
        {
            GameStart = true;
            ButtonsPressed = 0;
            PressureGauge.timeIncrease += Severity;
        }
        
    }

    private void ButtonGamePlay()
    {
        if (ButtonChosen == null)
        {
            //randomly chose a button from the list
            //make sure to not choose the same button twice in a row
            int ChosenButton;
            do
            {
                ChosenButton = Random.Range(0, Buttons.Count);
            }
            while (ChosenButton == PrevNum);
            PrevNum = ChosenButton;

            ButtonChosen = Buttons[ChosenButton];
            ButtonChosen.ToggleButton();

        }

        if (ButtonChosen != null && ButtonChosen.ButtonOn)
        {
            ButtonChosen = null;
            ButtonsPressed++;
        }
    }
}
