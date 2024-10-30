using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] Transform[] pathPoints;
    [SerializeField] int pointIndex = 0;
    [SerializeField] bool startGhostMovement = false;
    [SerializeField] float moveSpeed = 5;

    [SerializeField] NavMeshAgent agent;

    private bool ghostStartedMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startGhostMovement && !ghostStartedMoving)
        {
            ghostStartedMoving = true;
            StartCoroutine(MoveAlongPath());
        }
    }

    public void TriggerStartGhostMovement()
    {
        startGhostMovement = true;
    }

    IEnumerator MoveAlongPath()
    {
        for (pointIndex = 0; pointIndex < pathPoints.Length; pointIndex++)
        {
            transform.LookAt(pathPoints[pointIndex]);
            while (transform.position != pathPoints[pointIndex].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, pathPoints[pointIndex].position, moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

        }
    }

    public bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.0001;
    }
}
