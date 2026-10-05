using UnityEngine;
using UnityEngine.UI;

public class RadarButton : MonoBehaviour
{
    [SerializeField] RadarGame radarGame;
    private Button button;
    private void Start()
    {
        radarGame = GameObject.FindAnyObjectByType<RadarGame>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

    }
   

    public void OnButtonClick()
    {
        if (radarGame.score >= radarGame.scoreToWin) return;

        radarGame.score++;
        gameObject.SetActive(false);

        if (radarGame.score < radarGame.scoreToWin)
            radarGame.ActivateButton();
    }
}
