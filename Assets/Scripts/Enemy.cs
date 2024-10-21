using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    float distance;
    public bool isStuned = false;
    [SerializeField] int StunTime = 10;
    [SerializeField] float EnemySpeed = 3.5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = EnemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance < 10f)
        {
            agent.SetDestination(target.position);
        }
        onStun();
    }
    public double getEnemyX()
    {
        return transform.position.x;
    }
    public double getEnemyY()
    {
        return transform .position.y;
    }
    public void onStun()
    {
        if(isStuned)
        {
            Debug.Log("ENEMY STUNNED");
            agent.speed = 0;
            Debug.Log("Enemy Speed: "+ agent.speed);
            StartCoroutine(stunTime());
        }
    }

    IEnumerator stunTime()
    {
        Debug.Log("ENEMY TIME STUN STARTED");
        yield return new WaitForSeconds(StunTime);
        Debug.Log("ENEMY TIME STUN STOPPED");
        agent.speed = EnemySpeed;
        Debug.Log("Enemy After Speed: " + agent.speed);
        isStuned = false;
    }
}

