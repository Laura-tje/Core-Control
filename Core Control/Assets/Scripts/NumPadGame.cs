

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NumPadGame : MonoBehaviour, IGameInteractable
{
    //1- randomly give every button a color and a number
    [SerializeField] List<Sprite> Colors;
    [SerializeField] List<GameObject> Numbers;
    [SerializeField] GameObject TurnOnToPlay;
    
    public int AmountOfButtons = 9;
    public int Amount = 0;

    bool SetGood = false;
    bool SetBad = false;


    


    void Update()
    {
        

        if (ButtonVar.playing == true) {
        
            if(SetBad == false)
            {
                //PressureGauge.Instance.timeIncrease += 1f;
                SetBad = true;
                SetGood = false;
            }
        }


       if (Amount == AmountOfButtons)
        {
            
            Debug.Log("You Win!");
            ButtonVar.playing = false;
            TurnOnToPlay.SetActive(false);

        }
        
    }

    public void ShuffleColors()
    {
        //shuffle colors list
        for (int i = 0; i < Colors.Count; i++)
        {
            int rand = Random.Range(i, Colors.Count);
            (Colors[i], Colors[rand]) = (Colors[rand], Colors[i]);

        }

        for (int i = 0; i < Numbers.Count; i++)
        {
            //set the color of the number to the color in the shuffled list
            Numbers[i].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Colors[i];
            
        }
    }

    public void ActivateGame()
    {
        Amount = 0;
        ButtonVar.playing = true;
        TurnOnToPlay.SetActive(true);
        ShuffleColors();
        if (SetGood == false)
        {
            //PressureGauge.Instance.timeIncrease -= 1f;
            SetGood = true;
            SetBad = false;
        }
    }
}