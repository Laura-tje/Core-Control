using UnityEngine;

public class Fingerprintgame : MonoBehaviour, IGameInteractable
{
    [SerializeField] private GameObject fingerprint;
    [SerializeField] private Finger finger; // het lampje script

    private bool gameActive = false;

    void Start()
    {
        if (finger == null)
        {
            Debug.LogError("Fingerprintgame: het veld Finger is niet gevuld in de Inspector!");
        }
    }

    void Update()
    {
        if (finger == null) return;

        // game is klaar zodra Finger het probleem heeft opgelost
        if (gameActive && !finger.isProblem)
        {
            MiniGameFinished();
        }
    }

    // wordt aangeroepen door de PressureGauge zodra deze game kapot gaat
    public void ActivateGame()
    {
        if (gameActive) return;

        if (finger == null)
        {
            Debug.LogError("Fingerprintgame: ActivateGame kan niet, Finger ontbreekt!");
            return;
        }

        Debug.Log("Fingerprintgame: ActivateGame aangeroepen");

        gameActive = true;
        finger.isProblem = true;
    }

    public void buttonClicked()
    {
    }

    private void MiniGameFinished()
    {
        gameActive = false;

        Debug.Log("Fingerprintgame: opgelost, gauge wordt gemeld");

        if (PressureGauge.Instance != null)
        {
            PressureGauge.Instance.MiniGameSolved((int)PressureGauge.EGames.FingerprintGame);
        }
        else
        {
            Debug.LogError("Fingerprintgame: geen PressureGauge.Instance gevonden!");
        }
    }

    public void ChangeIndicator(Sprite indicator)
    {
        
    }
}