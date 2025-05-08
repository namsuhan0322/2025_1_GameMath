using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMover : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [SerializeField] private float duration = 2f;
    [SerializeField] private float t = 0f;

    void Update()
    {
        if (t < 1f)
        {
            //t += Time.deltaTime / duration;
            t = Mathf.PingPong(Time.time / duration, 1f);

            //transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
            transform.position = Vector3.LerpUnclamped(startPos.position, endPos.position, t);
        }
    }
}
