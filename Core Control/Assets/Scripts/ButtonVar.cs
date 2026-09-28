using UnityEngine;
using UnityEngine.UI;

public class ButtonVar : MonoBehaviour
{
    [SerializeField] public Sprite Color;
    private Image randomColor;
    NumPadGame numPadGame;
    private void Start()
    {
        randomColor = GameObject.Find("random color").GetComponent<Image>();
        numPadGame = GameObject.Find("NumPadGame").GetComponent<NumPadGame>();
    }

    private void Update()
    {
        Color = gameObject.GetComponent<UnityEngine.UI.Image>().sprite;
    }

    public void colorCheck()
    {
        if(Color == randomColor.sprite)
        {
            Debug.Log("Correct");
            randomColor.GetComponent<ShuffleColor>().UpdateColor();
            numPadGame.ShuffleColors();
        }
        else if (Color != randomColor.sprite)
        {
            Debug.Log("Incorrect");
        }
        else
        {
                       Debug.Log("Error");
        }
    }
}
