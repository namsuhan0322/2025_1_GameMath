using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public Transform player;

    public float viewDistance = 5f;
    public float viewAngle = 60f;   // �þ߰�

    void Update()
    {
        Vector3 topPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        float dot = forward.x * topPlayer.x + forward.y * topPlayer.y + forward.z * topPlayer.z;
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        float radians = angle * Mathf.Deg2Rad;

        Vector3 position = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * viewDistance;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (angle < viewAngle / 2 && distanceToPlayer <= viewDistance)
        {
            Debug.Log("�÷��̾ �þ� �ȿ� ����!");
        }
    }

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
