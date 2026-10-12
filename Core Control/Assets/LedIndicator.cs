using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LedIndicator : MonoBehaviour, IGameInteractable
{
    [SerializeField] bool open = true;
    [SerializeField] Image fill;
    [SerializeField] Sprite green;
    [SerializeField] Sprite red;
    [SerializeField] Image led;

    float timer;
    bool SetGood = false;
    bool SetBad = false;

    public bool correct;

    private void Start()
    {
        fill.fillAmount = 0.9f;
    }

    private void Update()
    {
        SetLight();
        
        timer += Time.deltaTime;
    }

    
    private void SetLight()
    {
        if(timer > 0.1f)
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
            timer = 0f;
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
            PressureGauge.Instance.MiniGameSolved((int)PressureGauge.EGames.DraaiknopGame);
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
