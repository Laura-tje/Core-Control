using UnityEngine;
using UnityEngine.UI;

public class LedIndicator : MonoBehaviour
{
    [SerializeField] bool open = true;
    [SerializeField] Image fill;
    [SerializeField] Sprite green;
    [SerializeField] Sprite red;
    [SerializeField] Image led;

    private void Update()
    {

        if (open)
        {
            //if open
            if (fill.fillAmount > 0.8f)
            {
                Debug.Log("LED is open");
                led.sprite = green;
            }
            else
            {
                led.sprite = red;
            }
        }
        else
        {
            //if closed
            if (fill.fillAmount < 0.2f)
            {
                Debug.Log("LED is closed");
                led.sprite = green;
            }
            else
            {
                led.sprite = red;
            }
        }
        
        
    }
}
