using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WireTaskManager : MonoBehaviour
{
    public int totalWires = 4;
    public List<WireEnd> ends;
    int connected;

    void Awake()
    {
        if (ends == null || ends.Count == 0)
        {
            Debug.LogError("WireTaskManager: 'ends' list is empty, assign the WireEnd objects in the Inspector.");
            return;
        }
        ShuffleEnds();
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

    public void WireConnected()
    {
        connected++;
        if (connected >= totalWires)
        {
            Debug.Log("Taak voltooid!");
        }
    }
}