using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform target;

    [SerializeField] private float targetSize = 30f;
    [SerializeField] private float minValue = 15f;
    [SerializeField] private float maxValue = 145f;

    public bool isCorrect { get; private set; }
    public bool isWrong { get; private set; }

    private void Start()
    {
        SetNewTarget();
    }

    public void CheckCorrect(float value)
    {
        if (isCorrect) return;

        float targetY = target.anchoredPosition.y;
        float halfSize = targetSize * 0.5f;

        if (value >= targetY - halfSize && value <= targetY + halfSize)
        {
            isCorrect = true;
            isWrong = false;

            if (SliderManager.Instance != null)
                SliderManager.Instance.SliderConnected(this);
        }
    }

    private void SetNewTarget()
    {
        Vector2 pos = target.anchoredPosition;
        pos.y = Random.Range(minValue, maxValue);
        target.anchoredPosition = pos;
        CheckCorrect(slider.value);
    }

    public void Break()
    {
        isCorrect = false;
        isWrong = true;
        SetNewTarget();
    }
}