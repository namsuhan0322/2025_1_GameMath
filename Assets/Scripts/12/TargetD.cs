using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    [SerializeField] private Ball ball;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ball = FindObjectOfType<Ball>();

        ballObj = new List<GameObject>();

        currentPlayer = PlayerType.Player1;
    }

    void Update()
    {
        speed = rb.velocity.magnitude;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if((currentPlayer == PlayerType.Player1 && hit.collider.CompareTag("Player1")) || 
                    (currentPlayer == PlayerType.Player2 && hit.collider.CompareTag("Player2")))
                {
                    rb = hit.collider.attachedRigidbody;

                    if (rb != null)
                    {
                        Vector3 point = hit.point;

                        Vector3 dir = (rb.position - point).normalized;

                        rb.AddForce(dir * forcePower, ForceMode.Impulse);

                        SwitchTurn();
                    }
                }
            }
        }
    }

    private void SwitchTurn()
    {
        currentPlayer = (currentPlayer == PlayerType.Player1) ? PlayerType.Player2 : PlayerType.Player1;

        ballObj.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            bool isDuplicate = ballObj.Any(ball => ball.name == collision.gameObject.name);

            if (!isDuplicate)
                ballObj.Add(collision.gameObject);

            if (ballObj.Count == 1)
            {
                score++;
            }
        }
    }
}
