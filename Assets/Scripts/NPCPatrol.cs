using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    [SerializeField] Transform[] waypoints;
    enum EnemyStates { Patrolling, Chasing }
    [SerializeField] EnemyStates currentState;                   // NPC State
    Transform objectToChase;                                            // 추격할 위치
    int currentWaypoint = 0;                                                // 순찰할  Waypoint 

    public void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        objectToChase = GameObject.FindGameObjectWithTag("Player").transform;       // Player 위치로 설정 
        if (_navMeshAgent == null)
            Debug.LogError("Nav Mesh Agent component not found attached to " + gameObject.name);
        else
        {
            // Patrolling 상태이면 현재 Waypoint로 이동
            if (currentState == EnemyStates.Patrolling) _navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
    public void Update()
    {
        // Player와 NPC의 거리가 10 이상이면 Patrolling 상태, 그렇지않으면 Chasing 상태로 변경
        if (Vector3.Distance(transform.position, objectToChase.position) > 10f) currentState = EnemyStates.Patrolling;
        else currentState = EnemyStates.Chasing;

        //  Patrolling 상태이면 순찰, Chasing 상태이면 Player 추격
        if (currentState == EnemyStates.Patrolling)
        {
            //  현재 Waypoint와의 남은 거리가 1 이하이면 다음 Waypoint로 변경
            if (_navMeshAgent.remainingDistance <= 1.0f)
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length) currentWaypoint = 0;
            }
            _navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
        else _navMeshAgent.SetDestination(objectToChase.position);
    }
}

