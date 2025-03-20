using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform earth;

    public float speed = 5f;
    public float radius = 30f;
    public float angle = 0f;

    void Update()
    {
        angle += speed * Time.deltaTime;
        float radians = angle * Mathf.Deg2Rad;

        Vector3 position = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * radius;
        transform.position = earth.position + position;
    }
}
