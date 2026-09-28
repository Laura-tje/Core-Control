using UnityEngine;

public class Finger : MonoBehaviour
{
    private bool isHolding = false;

    [SerializeField] private GameObject line;

    private Vector3 startposLine;
    private Vector3 endposLine;

    private float timer = 0f;
    private bool isProblem = false;

    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject greenLight;

    void Start()
    {
        startposLine = line.transform.position;

        endposLine = new Vector3(
            line.transform.position.x,
            line.transform.position.y - 60,
            line.transform.position.z
        );

        line.SetActive(false);
        redLight.SetActive(false);
        greenLight.SetActive(true);
        isProblem = true; ////////for testing
    }

    void Update()
    {
        redLight.SetActive(isProblem);
        greenLight.SetActive(!isProblem);
        
        if (isHolding)
        {
            line.SetActive(true);

            timer += Time.deltaTime;

            float t = timer / 2f;

            line.transform.position = Vector3.Lerp(
                startposLine,
                endposLine,
                t
            );
        }
        else
        {
            line.SetActive(false);
        }

        if (line.transform.position == endposLine)
        {
            isProblem = false;
        }
    }

    public void FingerOn()
    {
        isHolding = true;
        timer = 0f;

        line.transform.position = startposLine;
    }

    public void FingerOf()
    {
        isHolding = false;
    }
}