using System.Collections.Generic;
using UnityEngine;

public class CableMG : MonoBehaviour
{
    [SerializeField] private LineRenderer cable;

    [SerializeField] private Vector3 startPoint;

    private void Start()
    {
        startPoint = transform.position;
        if (cable == null)
        {
            cable = GetComponent<LineRenderer>();
            cable.SetPosition(0, startPoint);
            cable.SetPosition(1, startPoint);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;

        SetCablePosition(newPos);
    }

    private void OnMouseUp()
    {
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
}