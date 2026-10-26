using UnityEngine;
using UnityEngine.UI;

public class UIWave : MaskableGraphic
{
    [SerializeField] private float thickness = 4f;

    private Vector2[] points = new Vector2[0];

    public void SetPoints(Vector2[] newPoints)
    {
        points = newPoints;
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (points == null || points.Length < 2) return;

        float half = thickness * 0.5f;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 dir;
            if (i == 0) dir = points[1] - points[0];
            else if (i == points.Length - 1) dir = points[i] - points[i - 1];
            else dir = points[i + 1] - points[i - 1];

            dir.Normalize();
            Vector2 normal = new Vector2(-dir.y, dir.x) * half;

            UIVertex v = UIVertex.simpleVert;
            v.color = color;

            v.position = points[i] + normal;
            vh.AddVert(v);
            v.position = points[i] - normal;
            vh.AddVert(v);
        }

        for (int i = 0; i < points.Length - 1; i++)
        {
            int a = i * 2;
            vh.AddTriangle(a, a + 1, a + 2);
            vh.AddTriangle(a + 1, a + 3, a + 2);
        }
    }
}