using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow3D : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseHandle();
        }

        MovementHandle();
    }

    void MouseHandle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
        }
    }

    void MovementHandle()
    {
        Vector3 direction = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        Vector3 dirPos = direction - transform.position;

        Vector3 move = dirPos.normalized * speed * Time.deltaTime;

        transform.position += move;       
    }
}
