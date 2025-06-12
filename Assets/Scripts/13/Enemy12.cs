using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy12 : MonoBehaviour
{
    public float hp;
    public float maxHp;

    private void Start()
    {
        hp = maxHp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("QQ"))
        {
            PlayerAttack p = FindObjectOfType<PlayerAttack>();

            float takeDamage;
            takeDamage = p.damage;

            hp -= p.TakeDamage(takeDamage);

            if (hp <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
