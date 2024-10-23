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
    [SerializeField] PlayerControlls playerControlls;
    [SerializeField] private float rotationSpeed;
    Animator animatorLink;
    Rigidbody2D rigidLink;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = EnemySpeed;
        animatorLink = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance < 15f)
        {
            agent.SetDestination(target.position);
            animatorLink.SetFloat("Speed", agent.speed);
            if (!isStuned)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, agent.velocity);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        if(playerControlls.flashing == true && distance < 5) { 
            isStuned = true;
            onStun();
            
        }
        
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
            animatorLink.SetBool("Stun", isStuned);
        }
    }

    IEnumerator stunTime()
    {
        //Debug.Log("ENEMY TIME STUN STARTED");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(StunTime);
       // Debug.Log("ENEMY TIME STUN STOPPED");
        agent.speed = EnemySpeed;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        //Debug.Log("Enemy After Speed: " + agent.speed);
        isStuned = false;
        animatorLink.SetBool("Stun", isStuned);
    }

}

