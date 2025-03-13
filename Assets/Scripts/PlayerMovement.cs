using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        MovementHandle();
    }

    void MovementHandle()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, moveY, 0);

        float magnitude = Mathf.Sqrt(direction.x * direction.x + direction.y 
                                    * direction.y + direction.z * direction.z);

        if (magnitude > 0)
        {
            Vector3 normalized = direction / magnitude;
            Vector3 move = normalized * speed * Time.deltaTime;

            transform.position += move;
        }
    }
}
