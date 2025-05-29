using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierDeCasteljau : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    private List<Vector3> pointsPos = new List<Vector3>();

    private float timeValue = 0f;

    void Awake()
    {
        foreach (var pt in points)
        {
            if (pt != null)
                pointsPos.Add(pt.position);
        }
    }

    void Update()
    {
        timeValue += Time.deltaTime / 2f;
        transform.position = DeCasteljau(pointsPos, timeValue);
    }

    private Vector3 DeCasteljau(List<Vector3> p, float t)
    {
        while (p.Count > 1)
        {
            int last = p.Count - 1;

            var next = new List<Vector3>(last);
            for (int i = 0; i < last; i++)
            {
                next.Add(Vector3.Lerp(p[i], p[i + 1], t));
            }

            p = next;
        }

        return p[0];
    }
}
