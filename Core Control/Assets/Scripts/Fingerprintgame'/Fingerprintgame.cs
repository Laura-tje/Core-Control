using UnityEngine;

public class Fingerprintgame : MonoBehaviour, IGameInteractable
{
    [SerializeField] private GameObject fingerprint;
    [SerializeField] private Finger finger; //het lampje script

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
        //als de game actief is en Finger heeft het probleem opgelost, is de game klaar
        if (gameActive && !finger.isProblem)
        {
            MiniGameFinished();
            gameActive = false;
        }
    }

    //wordt aangeroepen door de PressureGauge zodra deze game kapot gaat
    public void ActivateGame()
    {
        //als de game al actief is, niks doen (voorkomt dat hij opnieuw start als dit vaker wordt aangeroepen)
        if (gameActive)
        {
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
}