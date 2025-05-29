using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Transform target;

    public GameObject lrPos;

    public List<Attack> attack = new List<Attack>();

    public LayerMask enemyLayer;

    [SerializeField] private float time = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, enemyLayer))
            {
                FindTartget(hit.transform);
            }
        }
    }

    private void FindTartget(Transform enemyTarget)
    {
        target = enemyTarget;

        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        for (int i = 0; i < attack.Count; i++)
        {
            attack[i].StartAttack();

            yield return new WaitForSeconds(0.2f);
        }
    }
}
