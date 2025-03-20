using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform enemy;
    public Transform player;

    public float radius = 30f;
    public float viewAngle = 60f;

    private void Start()
    {
        enemy = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        float dot = Vector3.Dot(forward, directionToPlayer);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        float radians = angle * Mathf.Deg2Rad;

        Vector3 position = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * radius;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (angle < viewAngle / 2 && distanceToPlayer <= radius)
        {
            enemy.transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else
        {
            enemy.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
