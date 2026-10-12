using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WireTaskManager : MonoBehaviour, IGameInteractable
{
    public static WireTaskManager Instance { get; private set; }

    public int totalWires = 4;
    public List<WireEnd> ends;
    public bool startConnected = false;

    [Tooltip("Hoeveel pixels hoogteverschil nog als 'zelfde lijn' telt.")]
    public float alignTolerance = 5f;

    const int MaxShuffleAttempts = 50;

    readonly List<WireStart> connectedWires = new List<WireStart>();

    bool isBroken;

    public int ConnectedCount => connectedWires.Count;
    public bool AllConnected => connectedWires.Count >= totalWires;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("WireTaskManager: er is al een instance, deze wordt verwijderd.");
            Destroy(this);
            return;
        }
        Instance = this;

        if (ends == null || ends.Count == 0)
        {
            Debug.LogError("WireTaskManager: 'ends' list is empty, assign the WireEnd objects in the Inspector.");
            return;
        }
        ShuffleEnds();
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    // Husselt de ends net zo lang tot geen enkele start op dezelfde lijn staat als zijn eigen kleur-end.
    void ShuffleEnds()
    {
        Canvas.ForceUpdateCanvases(); // zorg dat layouts hun echte posities hebben

        bool inLayout = ends[0].transform.parent.GetComponent<LayoutGroup>() != null;
        var starts = FindObjectsByType<WireStart>(FindObjectsSortMode.None);

        // De plekken waar de ends nu staan (alleen nodig buiten een layout)
        var slots = new List<Vector3>();
        foreach (var e in ends) slots.Add(e.transform.position);

        for (int attempt = 0; attempt < MaxShuffleAttempts; attempt++)
        {
            if (inLayout)
            {
                var order = new List<WireEnd>(ends);
                Shuffle(order);
                for (int i = 0; i < order.Count; i++)
                    order[i].transform.SetSiblingIndex(i);

                // De layout moet de nieuwe volgorde eerst verwerken voordat we posities kunnen vergelijken
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ends[0].transform.parent);
            }
            else
            {
                Shuffle(slots);
                for (int i = 0; i < ends.Count; i++)
                    ends[i].transform.position = slots[i];
            }

            if (!AnyStartAlignedWithEnd(starts))
                return;
        }

        Debug.LogWarning("WireTaskManager: geen opstelling gevonden zonder kabels op dezelfde lijn, laatste husseling blijft staan.");
    }

    // True als een start op dezelfde hoogte staat als een end van dezelfde kleur
    bool AnyStartAlignedWithEnd(WireStart[] starts)
    {
        foreach (var s in starts)
        {
            foreach (var e in ends)
            {
                if (e.wireColor == s.wireColor &&
                    Mathf.Abs(s.transform.position.y - e.transform.position.y) <= alignTolerance)
                    return true;
            }
        }
        return false;
    }

    static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public void WireConnected(WireStart wire)
    {
        if (!connectedWires.Contains(wire))
            connectedWires.Add(wire);

        if (isBroken && AllConnected)
        {
            isBroken = false;

            if (PressureGauge.Instance != null)
                PressureGauge.Instance.MiniGameSolved((int)PressureGauge.EGames.CableGame);
        }
    }

    public void WireDisconnected(WireStart wire)
    {
        connectedWires.Remove(wire);
    }

    public void ActivateGame()
    {
        isBroken = true;

        foreach (var wire in new List<WireStart>(connectedWires))
        {
            wire.Disconnect();
        }

        ShuffleEnds();
    }
}