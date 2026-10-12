using UnityEngine;

public class Fingerprintgame : MonoBehaviour, IGameInteractable
{
    [SerializeField] private GameObject fingerprint;
    [SerializeField] private Finger finger; //het lampje script

    private bool gameActive = false;

    void Update()
    {
        //als de game actief is en Finger heeft het probleem opgelost, is de game klaar
        if (gameActive && !finger.isProblem)
        {
            MiniGameFinished();
        }
    }

    //wordt aangeroepen door de PressureGauge zodra deze game kapot gaat
    public void ActivateGame()
    {
        gameActive = true;
        finger.isProblem = true;
    }

    public void buttonClicked()
    {

    }

    private void MiniGameFinished()
    {
        gameActive = false;

        if (PressureGauge.Instance != null)
        {
            PressureGauge.Instance.MiniGameSolved((int)PressureGauge.EGames.FingerprintGame);
        }
    }
}