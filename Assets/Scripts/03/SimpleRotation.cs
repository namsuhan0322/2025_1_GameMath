using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public float rotationSpeed = 90f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Rotate(0, input * rotationSpeed * Time.deltaTime, 0);
    }
}
