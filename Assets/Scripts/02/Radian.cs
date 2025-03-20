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
        Debug.Log($"45도 -> 라디안 : {radians}");

        float degreeValue = radianValue * Mathf.Rad2Deg;
        Debug.Log($"파이/3 라디안 -> 도 변환 : {degreeValue}");
    }

    void Update()
    {
        float radians = angle * Mathf.Deg2Rad;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            angle += 15f;

            Debug.Log($"오른쪽 : {angle}");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            angle -= 15f;
            Debug.Log($"왼쪽 : {angle}");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            speed += 1f;
            Debug.Log($"위 : {speed}");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            speed -= 1f;
            Debug.Log($"아래 : {speed}");
        }

        Vector3 radianDir = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
        transform.position += radianDir * speed * Time.deltaTime;
    }
}
