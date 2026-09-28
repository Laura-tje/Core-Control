using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NumPadGame : MonoBehaviour
{
    //1- randomly give every button a color and a number
    [SerializeField] List<Sprite> Colors;
    [SerializeField] List<GameObject> Numbers;
    ButtonVar ButtonVar;
    int rand;
    void Start()
    {
        ShuffleColors();
        //foreach (var number in Numbers)
        //{
        //    number.
        //}
    }

   
    void Update()
    {
        
    }

    private void ShuffleColors()
    {
        //shuffle colors list
        for (int i = 0; i < Colors.Count; i++)
        {
            rand = Random.Range(i, Colors.Count);
            (Colors[i], Colors[rand]) = (Colors[rand], Colors[i]);
            

        }

        for(int i = 0; i < Numbers.Count; i++)
        {
            //set the color of the number to the color in the shuffled list
            Numbers[i].gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Colors[i];
            (Numbers[i], Numbers[rand]) = (Numbers[rand], Numbers[i]);
            ButtonVar = Numbers[i].GetComponent<ButtonVar>();
            ButtonVar.Number = i;

        }
    }
}
