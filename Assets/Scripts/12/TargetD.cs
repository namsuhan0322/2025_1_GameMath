using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetD : MonoBehaviour
{
    public enum PlayerType { Player1, Player2 };
    public PlayerType currentPlayer;

    private Rigidbody rb;

    [SerializeField] private float forcePower = 10f;
    [SerializeField] private int score = 0;
    [SerializeField] private float speed = 0f;

    [SerializeField] private List<GameObject> ballObj;
    [SerializeField] private Ball[] ball;

    private bool isWaitingForStop = false; // �� ��ȯ ��� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballObj = new List<GameObject>();
        currentPlayer = PlayerType.Player1;
    }

    private void Update()
    {
        if (rb != null)
            speed = rb.velocity.magnitude;

        // Ŭ�� ��, ���� �÷��̾��� ���� �����ϰ� ���� ����
        if (Input.GetMouseButtonDown(0) && !isWaitingForStop)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if ((currentPlayer == PlayerType.Player1 && hit.collider.CompareTag("Player1")) ||
                    (currentPlayer == PlayerType.Player2 && hit.collider.CompareTag("Player2")))
                {
                    rb = hit.collider.attachedRigidbody;

                    if (rb != null)
                    {
                        Vector3 point = hit.point;
                        Vector3 dir = (rb.position - point).normalized;
                        rb.AddForce(dir * forcePower, ForceMode.Impulse);

                        isWaitingForStop = true;
                    }
                }
            }
        }

        // �÷��̾ ģ �� ��ü + ���������� ģ �� ��� ������� üũ
        if (isWaitingForStop && AllPlayerBallsAndRBStopped())
        {
            SwitchTurn();
            isWaitingForStop = false;
        }
    }


    private bool AllPlayerBallsAndRBStopped()
    {
        const float stopThreshold = 0.05f;

        // �÷��̾ ģ ��� ���� ������� Ȯ��
        bool allPlayerBallsStopped = ballObj.All(ball =>
        {
            var rigid = ball.GetComponent<Rigidbody>();
            return rigid != null && rigid.velocity.magnitude < stopThreshold;
        });

        // ��� ģ ��(rb)�� ������� Ȯ��
        bool currentBallStopped = rb != null && rb.velocity.magnitude < stopThreshold;

        return allPlayerBallsStopped && currentBallStopped;
    }


    private void SwitchTurn()
    {
        currentPlayer = (currentPlayer == PlayerType.Player1) ? PlayerType.Player2 : PlayerType.Player1;
        ballObj.Clear();
        Debug.Log($"Switched Turn to: {currentPlayer}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            bool isDuplicate = ballObj.Any(ball => ball.name == collision.gameObject.name);

            if (!isDuplicate)
                ballObj.Add(collision.gameObject);

            if (ballObj.Count == 2)
            {
                score++;
            }
        }
    }
}
