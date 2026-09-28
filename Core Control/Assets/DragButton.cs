using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragButton : MonoBehaviour
{
    [SerializeField] Transform handle;
    [SerializeField] Image fill;
    Vector3 MousePos;

    [SerializeField] float startAngle = 180f;   // waar de knop begint (linksonder)
    [SerializeField] float sweep = 275f;        // hoeveel graden de knop draait
    [SerializeField] float spriteOffset = 135f; // hoe je handle-sprite getekend is
    public void Drag()
    {
        Debug.Log("Dragging");
        MousePos = Input.mousePosition;
        Vector2 dir = MousePos - handle.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0)? (360 + angle) : angle;
        if (angle <= startAngle || angle >= sweep)
        {
            Quaternion r = Quaternion.AngleAxis(angle + spriteOffset, Vector3.forward);
            handle.rotation = r;
            angle = (angle >= sweep ? (angle - 360) : angle) + 35;
            fill.fillAmount = 0.75f - (angle/360f) ;

        }

    }
}
