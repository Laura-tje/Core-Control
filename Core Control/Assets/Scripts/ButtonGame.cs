using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonGame : MonoBehaviour, IGameInteractable
{
    public List<ButtonHandler> Buttons;

    [SerializeField] private ButtonHandler ButtonChosen;
    private int PrevNum = -1;

    [SerializeField] private int ButtonsPressed = 0;
    [SerializeField] private int ButtonsToPress = 10;

    [SerializeField] private bool GameStart = false;
    
    [SerializeField] PressureGauge PressureGauge;
    [SerializeField] private float Severity = 2f;

   

    private void Update()
    {

        if(GameStart == true)
        {
            if (ButtonsPressed == ButtonsToPress)
            {
                Debug.Log("You win!");
                GameStart = false;
                //PressureGauge.timeIncrease -= Severity;
            }
            else
            {
                ButtonGamePlay();
            }
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

    public void ActivateGame()
    {
        GameStart = true;
        ButtonsPressed = 0;
    }
}
