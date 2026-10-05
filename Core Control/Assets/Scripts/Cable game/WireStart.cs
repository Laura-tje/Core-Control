using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

// Zet dit op de linker kabelkant (Image). 'line' is een kind-Image met pivot (0, 0.5).
public class WireStart : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Color wireColor = Color.red;
    public RectTransform line;      // de "kabel" (Image, Raycast Target UIT)
    public Canvas canvas;
    public WireTaskManager manager;

    public bool Connected { get; private set; }

    void Start()
    {
        GetComponent<RawImage>().color = wireColor;
        line.GetComponent<RawImage>().color = wireColor;
        line.GetComponent<RawImage>().raycastTarget = false; // anders blokkeert de kabel de drop
        ResetLine();
    }

    public void OnBeginDrag(PointerEventData e) { }

    public void OnDrag(PointerEventData e)
    {
        if (Connected) return;
        DrawLine(e.position);
    }

    public void OnEndDrag(PointerEventData e)
    {
        if (Connected) return;

        var hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(e, hits);

        foreach (var hit in hits)
        {
            var end = hit.gameObject.GetComponent<WireEnd>();
            if (end != null && end.wireColor == wireColor && !end.Connected)
            {
                end.Connected = true;
                Connected = true;
                DrawLine(end.transform.position); // snap naar de aansluiting
                manager.WireConnected();
                return;
            }
        }
        ResetLine(); // fout of losgelaten in het niets
    }

    void DrawLine(Vector2 target)
    {
        Vector2 start = transform.position;          // Overlay canvas: wereldpositie = pixels
        Vector2 dir = target - start;
        float length = dir.magnitude / canvas.scaleFactor;

        line.position = start;
        line.sizeDelta = new Vector2(length, line.sizeDelta.y);
        line.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    void ResetLine()
    {
        line.position = transform.position;
        line.sizeDelta = new Vector2(0, line.sizeDelta.y);
        line.rotation = Quaternion.identity;
    }
}
