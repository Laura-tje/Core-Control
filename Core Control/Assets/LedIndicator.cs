using UnityEngine;
using UnityEngine.UI;

public class LedIndicator : MonoBehaviour, IGameInteractable
{
    [SerializeField] bool open = true;
    [SerializeField] Image fill;
    [SerializeField] Sprite green;
    [SerializeField] Sprite red;
    [SerializeField] Image led;


    bool SetGood = false;
    bool SetBad = false;

    public bool correct;

    
    private void Update()
    {
        SetLight();

    }

    
    private void SetLight()
    {
        if (open && fill.fillAmount > 0.8f)
        {
            True();

        }
        else if (!open && fill.fillAmount < 0.2f)
        {
            True();
        }
        else
        {
            False();
        }
    }

    private void True()
    {
        led.sprite = green;
        correct = true;
        if(SetGood == false)
        {
            //PressureGauge.Instance.timeIncrease -= 1f;
            SetGood = true;
            SetBad = false;
        }
    }

    private void False()
    {
        led.sprite = red;
        correct = false;
        if(SetBad == false)
        {
            //PressureGauge.Instance.timeIncrease += 1f;
            SetBad = true;
            SetGood = false;
        }
        
    }

    public void ActivateGame()
    {
        open = !open;
    }
}
