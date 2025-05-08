using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HitEnemy : MonoBehaviour
{
    private LineRenderer lr;
    public LayerMask enemyLayer;
    private Transform target;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private float time = 0f;

    void Start()
    { 
        lr.enabled = false;
    }

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.widthMultiplier = 0.05f;
        lr.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.blue
        };
    }

    void Update()
    {
        if (target != null)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, target.position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, enemyLayer))
            {
                FindTartget(hit.transform);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            time -= Time.deltaTime;
            if (time <= 0f)
            {
                lr.enabled = false;
                target = null;
            }
        }
    }

    private void FindTartget(Transform enemyTarget)
    {
        target = enemyTarget;
        lr.enabled = true;

        if (bulletPrefab != null && bulletSpawn != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            bullet.transform.LookAt(target.transform);
        }
    }
}
