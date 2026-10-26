using UnityEngine;
using UnityEngine.EventSystems;

public class Knob : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private float minAngle = -135f;
    [SerializeField] private float maxAngle = 135f;

    // 0 = helemaal links, 1 = helemaal rechts
    public float Value { get; private set; } = 0.5f;

    public void OnPointerDown(PointerEventData e)
    {
        UpdateFromPointer(e);
    }

    public void OnDrag(PointerEventData e)
    {
        UpdateFromPointer(e);
    }

    private void UpdateFromPointer(PointerEventData e)
    {
        Vector2 center = RectTransformUtility.WorldToScreenPoint(e.pressEventCamera, transform.position);
        Vector2 dir = e.position - center;

        // 0 graden = omhoog, rechtsom = positief
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        Value = Mathf.InverseLerp(minAngle, maxAngle, angle);
        transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
    }
}