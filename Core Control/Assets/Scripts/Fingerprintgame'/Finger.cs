using UnityEngine;

public class Finger : MonoBehaviour
{
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject greenLight;
    [SerializeField] private float holdDuration = 2f;
    [SerializeField] private float lineTravel = 60f;

    public bool isProblem = false;

    private bool isHolding = false;
    private float timer = 0f;
    private Vector3 startposLine;
    private Vector3 endposLine;

    void Start()
    {
        startposLine = line.transform.position;
        endposLine = startposLine + new Vector3(0f, -lineTravel, 0f);

        line.SetActive(false);
    }

    void Update()
    {
        // lampjes volgen altijd de status van het probleem
        redLight.SetActive(isProblem);
        greenLight.SetActive(!isProblem);

        if (!isHolding)
        {
            line.SetActive(false);
            return;
        }

        line.SetActive(true);
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / holdDuration);
        line.transform.position = Vector3.Lerp(startposLine, endposLine, t);

        // alleen oplossen als de vinger lang genoeg is vastgehouden
        if (t >= 1f && isProblem)
        {
            isProblem = false;
        }
    }

    // koppel aan Pointer Down
    public void FingerOn()
    {
        isHolding = true;
        timer = 0f;
        line.transform.position = startposLine;
    }

    // koppel aan Pointer Up
    public void FingerOf()
    {
        isHolding = false;
        timer = 0f;
        line.transform.position = startposLine;
    }
}