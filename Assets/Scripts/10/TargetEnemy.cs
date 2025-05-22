using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TargetEnemy : MonoBehaviour
{
    private LineRenderer lr;
    private Transform target;

    public GameObject lrPos;
    private Vector3 startPosition;
    private Quaternion startRotation;

    public LayerMask enemyLayer;
    public Vector3 offset = new Vector3(0f, 1f, -2f);

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float time = 0f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.widthMultiplier = 0.05f;
        lr.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.green
        };
    }

    void Start()
    {
        lr.enabled = false;

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (target != null)
        {
            lr.SetPosition(0, lrPos.transform.position);
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
                transform.position = startPosition;
                transform.rotation = startRotation;
            }
        }
    }

    void LateUpdate()
    {
        if (!target) return;

        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);

        float t = 1f - Mathf.Exp(-speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);

        Vector3 desired = target.position + target.TransformDirection(offset);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desired,
            ref velocity,
            smoothTime,
            speed,
            Time.deltaTime);
    }

    private void FindTartget(Transform enemyTarget)
    {
        target = enemyTarget;
        lr.enabled = true;
    }
}
