using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotCross : MonoBehaviour
{
    public Transform target;

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
        Vector3 cross = new Vector3(up.y * targetDir.z - up.z * targetDir.y,
            up.z * targetDir.x - up.x * targetDir.z,
            up.x * targetDir.y - up.y * targetDir.x);

        return Vector3.Dot(cross, up) > 0;
    }
}
