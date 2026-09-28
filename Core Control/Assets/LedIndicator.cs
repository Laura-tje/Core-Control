using UnityEngine;
using UnityEngine.UI;

public class LedIndicator : MonoBehaviour
{
    [SerializeField] bool open = true;
    [SerializeField] Image fill;
    [SerializeField] Sprite green;
    [SerializeField] Sprite red;
    [SerializeField] Image led;

    public bool correct;

    private void Update()
    {

        if (open && fill.fillAmount > 0.8f)
        {
           True();

        }
        else if(!open && fill.fillAmount < 0.2f)
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
