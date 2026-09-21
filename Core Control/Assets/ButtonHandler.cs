using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    [SerializeField] private bool ButtonOn = false;
    [SerializeField] private GameObject ButtonGreen;

    public void ToggleButton()
    {
        ButtonOn = !ButtonOn;
        if(ButtonOn)
        {
            ButtonGreen.SetActive(true);
        }
        else
        {
            ButtonGreen.SetActive(false);
        }
        //Debug.Log("Button state: " + (ButtonOn ? "On" : "Off"));
    }
}
