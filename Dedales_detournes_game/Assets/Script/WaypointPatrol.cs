using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    Animator m_Animator;
    int m_CurrentWaypointIndex;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_Animator.SetBool("IsWalk", false);
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
        m_Animator.SetBool("IsWalk", true);
    }
}
