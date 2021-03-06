using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public GameObject head;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer, whatIsWall;

    //Patroling
    //public Vector3 walkPoint;
    //bool walkPointSet;
    //public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    //public bool playerInSightRange;
    public bool playerInAttackRange;

    private void Awake()
    {        
        player = GameObject.Find("Player").transform;        
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (!playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();           
    }
    /*
    void Patroling() 
    {
        GetComponent<AudioSource>().enabled = true;
        if (!walkPointSet) 
            SearchWalkPoint();
        else if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    void SearchWalkPoint() 
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) 
            walkPointSet = true;
    }*/
    void ChasePlayer() 
    {
        GetComponent<AudioSource>().enabled = true;
        agent.SetDestination(player.position);
        transform.LookAt(player);
        head.transform.LookAt(player);
    }
    void AttackPlayer()
    {
        GetComponent<AudioSource>().enabled = false;
        head.transform.LookAt(player);
        if (!alreadyAttacked) 
        {
            var ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.transform.name != "Player")
                    ChasePlayer();
                else
                {
                    if (!alreadyAttacked) 
                    {
                        alreadyAttacked = true;
                        Invoke("Attack", timeBetweenAttacks);                        
                    }
                    agent.SetDestination(transform.position);
                }
            }
        }
    }   
    void Attack() 
    {
        FindObjectOfType<AudioManager>().Play("EnemyRange");
        Instantiate(projectile, head.transform.position, Quaternion.identity);
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, sightRange);
        //Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position, walkPoint);
    }
}