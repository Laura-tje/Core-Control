using UnityEngine;

[ExecuteAlways]
public class SetPositions : MonoBehaviour
{
    [SerializeField] private RectTransform[] sliders;
    [SerializeField] private RectTransform rectTransform;

    public bool Width;

    void Update()
    {
        if (sliders == null || sliders.Length == 0 || rectTransform == null)
            return;

        if (Width)
        {
            float width = rectTransform.rect.width;
            float spacing = width / sliders.Length;

            for (int i = 0; i < sliders.Length; i++)
            {
                Vector2 pos = sliders[i].anchoredPosition;
                pos.x = spacing * i + spacing * 0.5f; // centered in each segment
                sliders[i].anchoredPosition = pos;
            }
        }
        else
        {
            float height = rectTransform.rect.height;
            float spacing = height / sliders.Length;

            for (int i = 0; i < sliders.Length; i++)
            {
                Vector2 pos = sliders[i].anchoredPosition;
                pos.y = spacing * i + spacing * 0.5f;
                sliders[i].anchoredPosition = pos;
            }
        }
    }
}