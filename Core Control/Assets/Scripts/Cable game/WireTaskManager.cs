using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WireTaskManager : MonoBehaviour, IGameInteractable
{
    public static WireTaskManager Instance { get; private set; }

    public int totalWires = 4;
    public List<WireEnd> ends;
    public bool startConnected = false;

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

    void ShuffleEnds()
    {
        bool inLayout = ends[0].transform.parent.GetComponent<LayoutGroup>() != null;

        if (inLayout)
        {
            var order = new List<WireEnd>(ends);
            Shuffle(order);
            for (int i = 0; i < order.Count; i++)
                order[i].transform.SetSiblingIndex(i);
        }
        else
        {
            var slots = new List<Vector3>();
            foreach (var e in ends) slots.Add(e.transform.position);
            Shuffle(slots);
            for (int i = 0; i < ends.Count; i++)
                ends[i].transform.position = slots[i];
        }
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

    // IGameInteractable: koppelt één willekeurige verbonden kabel los.
    public void ActivateGame()
    {
        isBroken = true;

        if (connectedWires.Count == 0) return;   // alles is al los, het probleem staat al

        var wire = connectedWires[Random.Range(0, connectedWires.Count)];
        wire.Disconnect();   // meldt zichzelf af via WireDisconnected
    }
}   