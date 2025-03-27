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
        float moveX = Input.GetAxis("Vertical");
        float moveZ = Input.GetAxis("Horizontal");

        Vector3 moveDirection = (transform.forward * moveX) + (transform.right * moveZ);
        moveDirection.Normalize();

        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
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
        Debug.Log("�÷��̾ ó�� �������� �̵�");
        transform.position = startPos.position;
    }

    public void Finish()
    {
        Debug.Log("���� �߽��ϴ�");
    }
}
