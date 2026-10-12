using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform target;

    [SerializeField] private float targetSize = 30f;
    [SerializeField] private float minValue = 15f;
    [SerializeField] private float maxValue = 145f;
    [SerializeField] private float moveDuration = 0.5f;

    [Tooltip("Aan: de slider begint op de juiste plek. Uit: het doel beweegt naar een willekeurige plek zoals eerst.")]
    [SerializeField] private bool startCorrect = true;

    private Coroutine moveRoutine;

    public bool isCorrect { get; private set; }
    public bool isWrong { get; private set; }

    private void Start()
    {
        if (startCorrect)
            SnapToCorrect();
        else
            SetNewTarget();
    }

    // Zet het doel op een willekeurige plek en de slider er meteen middenin
    private void SnapToCorrect()
    {
        // Eerst de status, zodat CheckCorrect (bijv. via de onValueChanged van de slider)
        // de manager niet nog een keer meldt.
        isCorrect = true;
        isWrong = false;

        float newY = Random.Range(minValue, maxValue);

        Vector2 pos = target.anchoredPosition;
        pos.y = newY;
        target.anchoredPosition = pos;

        slider.value = Mathf.Clamp(newY, slider.minValue, slider.maxValue);
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
        float newY = Random.Range(minValue, maxValue);

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(MoveTargetTo(newY));
    }

    private IEnumerator MoveTargetTo(float targetY)
    {
        Vector2 startPos = target.anchoredPosition;
        Vector2 endPos = startPos;
        endPos.y = targetY;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            target.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / moveDuration);
            yield return null;
        }

        target.anchoredPosition = endPos;
        moveRoutine = null;

        CheckCorrect(slider.value);
    }

    public void Break()
    {
        isCorrect = false;
        isWrong = true;
        SetNewTarget();
    }
}