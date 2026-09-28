using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField] private LineRenderer cable;
    [SerializeField] private Vector3 startPoint;

    public bool isConnected { get; private set; }
    public bool isBroken { get; private set; }

    private void Start()
    {
        startPoint = transform.position;

        if (cable == null)
            cable = GetComponent<LineRenderer>();

        cable.SetPosition(0, startPoint);
        cable.SetPosition(1, startPoint);
    }

    private void OnMouseDrag()
    {
        if (isConnected) return;

        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, .2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                SetCablePosition(collider.transform.position);

                if (transform.parent == collider.transform.parent)
                {
                    isConnected = true;
                    isBroken = false;

                    if (CableManager.Instance != null)
                        CableManager.Instance.CableConnected(this);
                }
                return;
            }
        }

        SetCablePosition(newPos);
    }

    private void OnMouseUp()
    {
        if (isConnected) return;
        SetCablePosition(startPoint);
    }

    private void SetCablePosition(Vector3 newPos)
    {
        if (cable != null)
        {
            cable.SetPosition(0, startPoint);
            cable.SetPosition(1, newPos);
        }
    }

    public void Break()
    {
        isConnected = false;
        isBroken = true;
        SetCablePosition(startPoint);
    }
}