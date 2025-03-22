using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable]
public class Waypoint
{
    public Transform waypoint;
    public float idleTime;
    
    public float rotationSpeed;
}

public class EnemyController : MonoBehaviour
{
    private static readonly int Walk = Animator.StringToHash("Walk");

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Waypoint> waypoints;
    [SerializeField] private int currentWaypoint = 0;
    [SerializeField] private Animator animator;

    // [SerializeField] private float idleTime = 2f;
    // [SerializeField] private float rotationSpeed = 2f;

    private bool isWaiting;

    private void Start()
    {
        if (waypoints.Count > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].waypoint.position);
            animator.SetFloat(Walk, 1f);
        }
    }

    private void Update()
    {
        if (waypoints.Count == 0 || isWaiting) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        animator.SetFloat(Walk, 0f);

        float elapsedTime = 0;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, Random.Range(-45f, 45f), 0);

        while (elapsedTime < waypoints[currentWaypoint].idleTime)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, (elapsedTime / waypoints[currentWaypoint].idleTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        animator.SetFloat(Walk, 1f);
        agent.SetDestination(waypoints[currentWaypoint].waypoint.position);

        isWaiting = false;
    }
}