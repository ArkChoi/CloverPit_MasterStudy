using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMouseMove : MonoBehaviour
{
    NavMeshAgent agent;
    public void Start()
    {
        // get a reference to the player's Nav Mesh Agent component
        agent = GetComponent<NavMeshAgent>();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}