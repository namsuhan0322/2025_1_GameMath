using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectTest : MonoBehaviour
{
    public Vector3 velocity = new Vector3(2f, -3f, 0f);
    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    private float damping = 0.9f;

    void Update()
    {
        velocity += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal.normalized;

        float dot = Vector3.Dot(velocity, normal);
        Vector3 reflect = velocity - 2f * dot * normal;

        velocity = reflect * damping;
    }
}
