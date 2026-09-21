using System.Collections.Generic;
using Unity.VisualScripting;
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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, .2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                SetCablePosition(collider.transform.position);
                return;
            }
        }

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