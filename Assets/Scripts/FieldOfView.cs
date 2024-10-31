using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    [SerializeField] float moveSpeed = 10;

    Animator animator;
    [SerializeField] bool isChasing = false;
    bool hasKilledPlayer = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("FindTargetsWithDelay", .05f);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (!hasKilledPlayer)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        if (hasKilledPlayer) 
            { return; }

        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);

                    MoveToTarget(target);
                } else
                {
                    animator.SetBool("IsWalking", false);
                }
            } else
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void MoveToTarget(Transform target)
    {
        isChasing = true;
        Debug.Log("Start move to target");

        Debug.Log("Chasing player!!");
        animator.SetBool("IsWalking", true);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, 0, target.transform.position.z), moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

        if (transform.position == target.position || !isChasing || hasKilledPlayer)
        {
            animator.SetBool("IsWalking", false);
            animator.SetTrigger("IsKillingPlayer");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        if (other.CompareTag("Player"))
        {
            hasKilledPlayer = true;
            Debug.Log("Kill player!!!");
            isChasing = false;

            animator.SetTrigger("IsKillingPlayer");

            Player player = other.GetComponent<Player>();
            player.Die("a Serial Killer", transform);
        }
    }
}