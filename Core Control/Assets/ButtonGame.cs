using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonGame : MonoBehaviour
{
    public List<ButtonHandler> Buttons;

    [SerializeField] private ButtonHandler ButtonChosen;
    private int PrevNum = -1;

    private void Start()
    {
        Debug.Log("ButtonGame started with " + Buttons.Count + " buttons.");
    }

    private void Update()
    {
        if(ButtonChosen == null)
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

        if(ButtonChosen != null && ButtonChosen.ButtonOn)
        {
            ButtonChosen = null;
        }
    }
}
