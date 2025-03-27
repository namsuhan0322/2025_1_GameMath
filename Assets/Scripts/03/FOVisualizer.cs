using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVisualizer : MonoBehaviour
{
    public float viewAngle = 60f;
    public float viewDistance = 5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 forward = transform.forward * viewDistance;

        // ���� �þ� ���
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * forward;
        // ������ �þ� ���
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * forward;

        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);

        // ĳ���� ���� ����
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, forward);
    }
}
