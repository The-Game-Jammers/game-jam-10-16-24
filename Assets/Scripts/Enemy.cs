using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    float distance;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance < 10f)
        {
            agent.SetDestination(target.position);
        }
    }
}
