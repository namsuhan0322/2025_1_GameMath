using UnityEngine;

public class NoDot : MonoBehaviour
{
    public Transform player;
    public float viewAngle = 60f;   // �þ߰�

    void Update()
    {
        Vector3 topPlayer = (player.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        float dot = forward.x * topPlayer.x + forward.y * topPlayer.y + forward.z * topPlayer.z;
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (angle < viewAngle / 2)
        {
            Debug.Log("�÷��̾ �þ� �ȿ� ����!");
        }
    }
}
