using UnityEngine;
using UnityEngine.UI;

public class LedIndicator : MonoBehaviour
{
    [SerializeField] bool open = true;
    [SerializeField] Image fill;
    [SerializeField] Sprite green;
    [SerializeField] Sprite red;
    [SerializeField] Image led;

    [SerializeField]  float timer = 0f;
    float timeToWait = 15f;

    [SerializeField] private float minTime = 10f;
    [SerializeField] private float maxTime = 30f;

    bool SetGood = false;
    bool SetBad = false;

    public bool correct;

    private void Update()
    {
        timer += Time.deltaTime;
        SetLight();
        SetMiniGame();


        
        // Gefixed
        PressureGauge.Instance.timeIncrease -= 1f;

    }

    private void SetMiniGame()
    {
        if (correct && timer > timeToWait)
        {
            timer = 0f;
            timeToWait = Random.Range(minTime, maxTime);
            open = !open; 
            //presure gadge increasres
            PressureGauge.Instance.timeIncrease += 1f;
        }
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
            PressureGauge.Instance.timeIncrease -= 1f;
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
            PressureGauge.Instance.timeIncrease += 1f;
            SetBad = true;
            SetGood = false;
        }
        
    }
}
