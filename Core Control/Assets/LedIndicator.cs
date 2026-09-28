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

    public bool correct;

    private void Update()
    {
        timer += Time.deltaTime;
        SetLight();
        SetMiniGame();

    }

    private void SetMiniGame()
    {
        if (correct && timer > timeToWait)
        {
            timer = 0f;
            timeToWait = Random.Range(minTime, maxTime);
            open = !open;
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
    }

    private void False()
    {
        led.sprite = red;
        correct = false;
    }
}
