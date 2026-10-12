using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WireTaskManager : MonoBehaviour, IGameInteractable
{
    public static WireTaskManager Instance { get; private set; }

    public int totalWires = 4;
    public List<WireEnd> ends;
    public bool startConnected = false;   // vink aan in de Inspector om pre-solved te starten

    // Houdt bij welke kabels op dit moment verbonden zijn
    readonly List<WireStart> connectedWires = new List<WireStart>();

    // true zodra de gauge deze game kapot heeft gemaakt en hij nog niet is opgelost
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
            // Shuffle the order; the layout group places them
            var order = new List<WireEnd>(ends);
            Shuffle(order);
            for (int i = 0; i < order.Count; i++)
                order[i].transform.SetSiblingIndex(i);
        }
        else
        {
            // Shuffle the world positions the ends occupy
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

    // Wordt aangeroepen door WireStart
    public void WireConnected(WireStart wire)
    {
        if (!connectedWires.Contains(wire))
            connectedWires.Add(wire);

        // Alles verbonden na een breuk: meld de gauge dat het opgelost is.
        // Bij een nieuwe ActivateGame() kan het weer kapot en daarna opnieuw opgelost worden.
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

    // IGameInteractable: koppelt alle verbonden kabels los.
    public void ActivateGame()
    {
        isBroken = true;

        // Kopie, want Disconnect() haalt de kabel uit connectedWires
        foreach (var wire in new List<WireStart>(connectedWires))
            wire.Disconnect();
    }
}