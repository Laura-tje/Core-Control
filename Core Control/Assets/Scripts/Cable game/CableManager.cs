using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public static CableManager Instance { get; private set; }

    public int cablesBroken;

    [SerializeField] private float minBreakInterval = 3f;
    [SerializeField] private float maxBreakInterval = 8f;

    [SerializeField] private Transform[] cableEnds;
    [SerializeField] private Transform[] endpointSlots;

    private Cable[] cables;
    private readonly List<Cable> brokenCables = new List<Cable>();
    private Coroutine breakRoutine;
    private bool minigameActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        RandomizeEndPositions();
        cables = GetComponentsInChildren<Cable>();

        minigameActive = true;

        ConnectAllCables();

        breakRoutine = StartCoroutine(BreakRoutine());
    }

    private void ConnectAllCables()
    {
        foreach (Cable cable in cables)
        {
            Transform parent = cable.transform.parent;
            if (parent == null) continue;

            foreach (Transform sibling in parent)
            {
                if (sibling == cable.transform) continue;

                cable.Connect(sibling.position);
                break;
            }
        }

        UpdateBrokenCount();
        CheckAllConnected();
    }

    private void RandomizeEndPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (Transform slot in endpointSlots)
            positions.Add(slot.position);

        for (int i = positions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (positions[i], positions[j]) = (positions[j], positions[i]);
        }

        for (int i = 0; i < cableEnds.Length; i++)
            cableEnds[i].position = positions[i];
    }

    private IEnumerator BreakRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minBreakInterval, maxBreakInterval));
            BreakRandomCable();
        }
    }

    private void BreakRandomCable()
    {
        List<Cable> connected = new List<Cable>();
        foreach (Cable cable in cables)
        {
            if (cable.isConnected)
            connected.Add(cable);
        }

        if (connected.Count == 0) return;

        Cable victim = connected[Random.Range(0, connected.Count)];
        victim.Break();

        if (!minigameActive)
        {
            minigameActive = true;
        }

        UpdateBrokenCount();
    }

    private void UpdateBrokenCount()
    {
        cablesBroken = 0;
        foreach (Cable cable in cables)
        {
            if (cable.isBroken)
                cablesBroken++;
        }
    }

    public void CableConnected(Cable cable)
    {
        UpdateBrokenCount();

        CheckAllConnected();
    }

    private void CheckAllConnected()
    {
        foreach (Cable cable in cables)
        {
            if (!cable.isConnected)
                return;
        }

        if (minigameActive)
        {
            minigameActive = false;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}