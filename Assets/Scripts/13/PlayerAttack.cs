using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform attackPos;

    public float damage = 5f;

    public Text damageText;

    void Start()
    {
        damageText.text = $"damage : {damage}";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(bulletPrefab, attackPos);
        }

        UpDownDamage();
    }

    public void UpDownDamage()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            damage++;
            damageText.text = $"damage : {damage}";
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            damage--;
            damageText.text = $"damage : {damage}";
        }
    }

    private void RoundPlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
        }
    }

    public float TakeDamage(float amount)
    {
        amount = damage;
        return amount;
    }
}
