using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent Mob;
    public float MobRadius= 7f;
    public float health = 100f;
    public GameObject blake;
    public Transform player;
    [SerializeField]
    private float attackDistance = 1.5f;
    public LayerMask isGround, isPlayer;
    [SerializeField]
    private Animator animator;
    public Transform[] waypoints;
    private int waypointIndex;
    private Vector3 target;
    public PlayerMovement playermovement;
    public int damageAmount = 20;
    public float attackCooldown = 1f;
    public bool canAttack = true;
    
    // Patrolling
    public Vector3 walkPoint;

    private bool walkPointSet;

    public float walkPointRange;
    
    // Attacking
    public float damagePerSecond;
    private bool wasAttacked;

    // States
    public float sightRange, attackRange;

    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
        UpdateDestination();
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playermovement = blake.gameObject.GetComponent<PlayerMovement>();


    }

    // Update is called once per frame
    void Update()
    {
        // Check for Sight Range and Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);
        

        float distanceToPlayer = Vector3.Distance(transform.position, blake.transform.position);

        if (distanceToPlayer < MobRadius)
        {
            Debug.Log("Hi");
            Debug.Log(distanceToPlayer);
            animator.SetBool("isRunning", true);
            playerInSightRange = (distanceToPlayer < sightRange);
            playerInAttackRange = (distanceToPlayer < attackRange);
            
            Vector3 dirToPlayer = transform.position - blake.transform.position;

            Vector3 newPosition = transform.position - dirToPlayer;

            Mob.SetDestination(newPosition);
            if (distanceToPlayer <= attackDistance )   
            {
                Debug.Log(gameObject.name);
                Debug.Log("okay");
                if (canAttack)
                {
                    blake.GetComponent<PlayerMovement>().TakeDamage(damageAmount);
                    canAttack = false;
                    StartCoroutine(AttackCooldown());
                }
                
                
            }
            if (playerInSightRange)
            {
                Debug.Log("Hey");
                if (playerInAttackRange)
                {
                    Debug.Log("what");
                    AttackingPlayer();
                }
                else
                {
                    Debug.Log("why");
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
                Debug.Log("how");
                IterateWaypointIndex();
                UpdateDestination();
            }
            else
            {
                // find new patrol point
                UpdateDestination();
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
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
        transform.LookAt(blake.transform.position);

        if (!wasAttacked && playerInAttackRange)
        {
            health -= playermovement.playerHealth;
            Debug.Log(health);
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
