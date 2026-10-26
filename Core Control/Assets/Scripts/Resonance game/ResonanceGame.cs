using UnityEngine;

public class ResonanceGame : MonoBehaviour, IGameInteractable
{
    [Header("Referenties")]
    [SerializeField] private Knob knob;
    [SerializeField] private UIWave wave;

    [Header("Frequentie")]
    [SerializeField] private float minFrequency = 1f;
    [SerializeField] private float maxFrequency = 8f;

    [Header("Oplossen")]
    [SerializeField] [Range(0.01f, 0.2f)] private float tolerance = 0.04f;
    [SerializeField] private float holdTime = 1.5f;

    [Header("Golf (amplitude en ruis als fractie van de hoogte)")]
    [SerializeField] private int points = 120;
    [SerializeField] [Range(0.05f, 0.5f)] private float amplitude = 0.3f;
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] [Range(0f, 0.5f)] private float maxNoise = 0.25f;
    [SerializeField] private Color stableColor = Color.green;
    [SerializeField] private Color unstableColor = Color.red;

    private bool gameActive = false;
    private float resonanceFrequency;
    private float stableTimer = 0f;
    private Vector2[] buffer;

    public float Mismatch { get; private set; } = 0f;
    public bool IsActive => gameActive;

    void Start()
    {
        if (knob == null || wave == null)
        {
            Debug.LogError("ResonanceGame: Knob of UIWave is niet gevuld in de Inspector!");
            enabled = false;
            return;
        }

        buffer = new Vector2[points];
    }

    void Update()
    {
        float frequency = Mathf.Lerp(minFrequency, maxFrequency, knob.Value);

        if (gameActive)
        {
            float range = maxFrequency - minFrequency;
            Mismatch = Mathf.Clamp01(Mathf.Abs(frequency - resonanceFrequency) / range * 2f);
            CheckSolved(frequency, range);
        }
        else
        {
            Mismatch = 0f;
        }

        DrawWave(frequency);
    }

    public void ChangeIndicator(Sprite indicator)
    {
        Debug.Log("Dees heb ik helemaal niet nodig, denk ik");
    }

    private void CheckSolved(float frequency, float range)
    {
        bool closeEnough = Mathf.Abs(frequency - resonanceFrequency) / range <= tolerance;

        stableTimer = closeEnough ? stableTimer + Time.deltaTime : 0f;

        if (stableTimer >= holdTime)
        {
            MiniGameFinished();
        }
    }

    private void DrawWave(float frequency)
    {
        Rect rect = wave.rectTransform.rect;
        float t = Time.time;

        for (int i = 0; i < points; i++)
        {
            float x01 = i / (float)(points - 1);
            float x = (x01 - 0.5f) * rect.width;

            float y01 = Mathf.Sin(x01 * frequency * Mathf.PI * 2f - t * scrollSpeed) * amplitude;

            float noise = (Mathf.PerlinNoise(x01 * 12f, t * 6f) - 0.5f) * 2f;
            y01 += noise * Mismatch * maxNoise;

            buffer[i] = new Vector2(x, y01 * rect.height);
        }

        wave.color = Color.Lerp(stableColor, unstableColor, Mismatch);
        wave.SetPoints(buffer);
    }

    // wordt aangeroepen door de PressureGauge zodra deze game kapot gaat
    public void ActivateGame()
    {
        if (gameActive) return;

        if (knob == null || wave == null)
        {
            Debug.LogError("ResonanceGame: Knob of UIWave ontbreekt in de Inspector!");
            return;
        }

        Debug.Log("ResonanceGame: ActivateGame aangeroepen");

        float current = Mathf.Lerp(minFrequency, maxFrequency, knob.Value);
        float range = maxFrequency - minFrequency;

        resonanceFrequency = Random.Range(minFrequency, maxFrequency);
        if (Mathf.Abs(resonanceFrequency - current) < range * 0.25f)
        {
            resonanceFrequency = current < (minFrequency + maxFrequency) * 0.5f
                ? maxFrequency - range * 0.1f
                : minFrequency + range * 0.1f;
        }

        stableTimer = 0f;
        gameActive = true;
    }

    private void MiniGameFinished()
    {
        gameActive = false;
        Mismatch = 0f;

        Debug.Log("ResonanceGame: opgelost, gauge wordt gemeld");

        if (PressureGauge.Instance != null)
        {
            PressureGauge.Instance.MiniGameSolved((int)PressureGauge.EGames.ResonanceGame);
        }
        else
        {
            Debug.LogError("ResonanceGame: geen PressureGauge.Instance gevonden!");
        }
    }
}