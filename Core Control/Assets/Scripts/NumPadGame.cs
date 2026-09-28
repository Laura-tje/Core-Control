

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NumPadGame : MonoBehaviour
{
    //1- randomly give every button a color and a number
    [SerializeField] List<Sprite> Colors;
    [SerializeField] List<GameObject> Numbers;
    [SerializeField] GameObject TurnOnToPlay;
    
    public int AmountOfButtons = 9;
    public int Amount = 0;
    public float breakTime = 0.5f;
    public float timer = 0f;



    void Start()
    {
        ShuffleColors();
        
    }


    void Update()
    {
        timer += Time.deltaTime;

        if(ButtonVar.playing == false && timer <= breakTime)
        {
            return;
        }
        if(timer >= breakTime && ButtonVar.playing == false)
        {
            Amount = 0;
            ButtonVar.playing = true;
            timer = 0f;
            ButtonVar.playing = true;
            TurnOnToPlay.SetActive(true);
        }
        else if (Amount == AmountOfButtons)
        {
            timer = 0f;
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
}