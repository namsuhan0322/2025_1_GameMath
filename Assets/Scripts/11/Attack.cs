using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform p0;
    public Transform p3;

    [Header("·£´ý ¹üÀ§")]
    public float p1Radius = 2f;
    public float p2Radius = 2f;
    public float p1Height = 3f;
    public float p2Height = 3f;

    [HideInInspector] public Vector3 p1;
    [HideInInspector] public Vector3 p2;

    private List<Vector3> points;

    public float speed = 2f;
    private float time = 0f;
    private bool isAttacking = false;

    private void Update()
    {
        if (!isAttacking) return;

        time += Time.deltaTime * speed;
        transform.position = DeCasteIjau(points, time);

        if (time >= 1f)
        {
            isAttacking = false;
        }
    }

    public void StartAttack()
    {
        time = 0f;
        isAttacking = true;

        GenerateRandomControlPoints();
        points = new List<Vector3> { p0.position, p1, p2, p3.position };
    }

    private void GenerateRandomControlPoints()
    {
        Vector2 rand1 = Random.onUnitSphere * p1Radius;
        p1 = p0.position + new Vector3(rand1.x, 0f, rand1.y);
        p1.y += p1Height;

        Vector2 rand2 = Random.onUnitSphere * p2Radius;
        p2 = p3.position + new Vector3(rand2.x, 0f, rand2.y);
        p2.y += p2Height;
    }

    public Vector3 DeCasteIjau(List<Vector3> p, float t)
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
