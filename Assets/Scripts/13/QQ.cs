using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class QQ : MonoBehaviour
{
    public Vector3 velocity = new Vector3(2f, -3f, 0f);
    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    public float damping = 0.9f;

    [Header("¶¥ Ã¼Å©")]
    [SerializeField] private int checkCountGround;
    [SerializeField] private float groundCheckDistance = 1.0f;
    [SerializeField] private LayerMask ground;

    void Update()
    {
        velocity += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal.normalized;

        float dot = Vector3.Dot(velocity, normal);
        Vector3 reflect = velocity - 2f * dot * normal;

        velocity = reflect * damping;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else
        {
            NonEnemy();

            if (checkCountGround == 3)
            {
                Destroy(gameObject);
            }
        }
    }

    private void NonEnemy()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, ground))
            checkCountGround++;
    }

    public float TakeDamage(float amount)
    {
        PlayerAttack p = FindObjectOfType<PlayerAttack>();
        amount = p.damage;
        return amount;
    }
}
