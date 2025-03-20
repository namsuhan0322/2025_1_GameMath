using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radian : MonoBehaviour
{
    public float degrees = 45f;
    public float radianValue = 1f;

    public float speed = 5f;
    public float angle = 30f;

    void Start()
    {
        float radians = degrees * Mathf.Deg2Rad;
        Debug.Log($"45�� -> ���� : {radians}");

        float degreeValue = radianValue * Mathf.Rad2Deg;
        Debug.Log($"����/3 ���� -> �� ��ȯ : {degreeValue}");
    }

    void Update()
    {
        float radians = angle * Mathf.Deg2Rad;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            angle += 15f;

            Debug.Log($"������ : {angle}");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            angle -= 15f;
            Debug.Log($"���� : {angle}");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            speed += 1f;
            Debug.Log($"�� : {speed}");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            speed -= 1f;
            Debug.Log($"�Ʒ� : {speed}");
        }

        Vector3 radianDir = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
        transform.position += radianDir * speed * Time.deltaTime;
    }
}
