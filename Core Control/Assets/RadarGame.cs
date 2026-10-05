using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public interface IGameInteractable
{
    void ActivateGame();
}
public class RadarGame : MonoBehaviour, IGameInteractable
{
    [SerializeField] bool isRadarActive = false;
    public GameObject ChosenButton;
    [SerializeField] GameObject[] Buttons;

    public int score = 0;
    public int scoreToWin = 5;

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
}
