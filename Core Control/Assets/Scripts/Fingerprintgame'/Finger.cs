using UnityEngine; 
using UnityEngine.EventSystems;

public class Finger : MonoBehaviour
{
    private bool isHolding = false;
    [SerializeField] private GameObject line;
    private Vector3 startposLine;
    private Vector3 endposLine;
    private bool startAnim;
    void Start()
    {
        startposLine = line.transform.position;
        endposLine = new Vector3(line.transform.position.x, line.transform.position.y - 30, line.transform.position.z);
        line.SetActive(false);
        startAnim = false;
    }

    void Update()
    {
        Debug.Log(isHolding);

        if (isHolding)
        {
            startAnim = false;
            line.SetActive(true);
            line.transform.position = Vector3.Lerp(startposLine, endposLine, 3 * Time.deltaTime);
        }
        else
        {
            line.SetActive(false);
        }
    }

    public void FingerOn()
    {
        isHolding = true;
        line.transform.position = startposLine;
    }

    public void FingerOf()
    {
        isHolding = false;
    }
}
