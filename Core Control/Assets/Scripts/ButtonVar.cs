using UnityEngine;
using UnityEngine.UI;

public class ButtonVar : MonoBehaviour
{
    [SerializeField] public Sprite Color;
    private Image randomColor;
    NumPadGame numPadGame;
    public static bool playing = false;
    private void Awake()
    {
        randomColor = GameObject.Find("random color").GetComponent<Image>();
        numPadGame = GameObject.Find("NumPadGame").GetComponent<NumPadGame>();
    
    }
    private void Start()
    {
        randomColor.gameObject.SetActive(false);
    }
    

    private void Update()
    {
        Color = gameObject.GetComponent<UnityEngine.UI.Image>().sprite;
    }

    public void colorCheck()
    { if(!playing) return;
        if (Color == randomColor.sprite)
        {
            Debug.Log("Correct");
            randomColor.GetComponent<ShuffleColor>().UpdateColor();
            numPadGame.ShuffleColors();
            numPadGame.Amount++;
        }
        else
        {
            Debug.Log("Incorrect");
        }

    }
}
