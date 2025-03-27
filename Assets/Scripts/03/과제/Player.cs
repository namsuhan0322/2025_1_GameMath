using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 90f;

    public Transform startPos;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovementHandle();
        RotationHandle();
    }

    void MovementHandle()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0 ,moveZ).normalized;

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    void RotationHandle()
    {
        float rotation = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotation = -rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotation = rotationSpeed * Time.deltaTime;
        }

        if (rotation != 0f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, rotation, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    public void ReturnStart()
    {
        Debug.Log("플레이어가 처음 시점으로 이동");
        transform.position = startPos.position;
    }

    public void Finish()
    {
        Debug.Log("도착 했습니다");
    }
}
