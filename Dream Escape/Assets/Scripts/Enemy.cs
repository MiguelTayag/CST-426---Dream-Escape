using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent Mob;
    public float MobRadius= 7f;
    public float health = 100f;
    public GameObject Player;
    public Transform player;
    public LayerMask isGround, isPlayer;
    [SerializeField]
    private Animator animator;
    public Transform[] waypoints;
    private int waypointIndex;
    private Vector3 target;
    
    
    // Patrolling
    public Vector3 walkPoint;

    private bool walkPointSet;

    public float walkPointRange;
    
    // Attacking
    public float damagePerSecond;
    private bool wasAttacked;
    public GameObject projectile;
    
    // States
    public float sightRange, attackRange;

    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
        UpdateDestination();
        animator = gameObject.GetComponent<Animator>();
        //Debug.Log("animator: " + animator.name);
        //animator.SetBool("isRunning", true);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Sight Range and Attack Range
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);
        

        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (distanceToPlayer < MobRadius)
        {
            animator.SetBool("isRunning", true);
            playerInSightRange = (distanceToPlayer < sightRange);
            playerInAttackRange = (distanceToPlayer < attackRange);
            
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPosition = transform.position - dirToPlayer;

            Mob.SetDestination(newPosition);
            
            if (playerInSightRange)
            {
                if (playerInAttackRange)
                {
                    AttackingPlayer();
                }
                else
                {
                    ChasingPlayer();
                }
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            // patrol path
            if (Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
            else
            {
                // find new patrol point
                UpdateDestination();
            }
        }
        
        //if(!playerInSightRange && !playerInAttackRange) Patrolling();
        /*if (playerInSightRange && !playerInAttackRange)
        {
            animator.SetBool("isRunning", true);
            //animator.SetBool("isWalking", false);
            ChasingPlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            //animator.SetBool("isRunning", false);
            //animator.SetBool("isWalking", true);
            AttackingPlayer();
        }*/
        //animator.SetBool("isRunning", !playerInAttackRange);
        //Debug.Log("end of update");
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        Mob.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    /*private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            Mob.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }*/

    private void SearchWalkPoint()
    {
        // Calculates a random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
            walkPointSet = true;
    }

    private void ChasingPlayer()
    {
        
        Mob.SetDestination(player.position);
    }

    private void AttackingPlayer()
    {
        Mob.SetDestination(transform.position);
        //animator.SetBool("isRunning", true);
        transform.LookAt(player);

        if (!wasAttacked)
        {
            // Attack code
            Rigidbody rb = Instantiate(projectile,transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            wasAttacked = true;
            Invoke(nameof(ResetAttack), damagePerSecond);
        }
    }

    private void ResetAttack()
    {
        wasAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
