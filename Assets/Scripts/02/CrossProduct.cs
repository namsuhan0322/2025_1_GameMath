using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CrossProduct : MonoBehaviour
{
    void Update()
    {
        Vector3 playerForward = transform.position;
        Vector3 toTarget = (transform.position - transform.position).normalized;

        if (IsLeft(playerForward, toTarget, Vector3.up))
        {
            Debug.Log("타겟이 플레이어 기준 왼쪽에 있음");
        }
        else
        {
            Debug.Log("타겟이 플레이어 기준 오른쪽에 있음");
        }
    }

    bool IsLeft(Vector3 forward, Vector3 targetDir, Vector3 up)
    {
        Vector3 cross = Vector3.Cross(forward, targetDir);

        return Vector3.Dot(cross, up) > 0;
    }
}
