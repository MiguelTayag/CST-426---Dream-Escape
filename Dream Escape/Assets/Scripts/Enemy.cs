using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent Mob;
    public float MobRadius= 7f;

    public GameObject Player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < MobRadius)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPosition = transform.position - dirToPlayer;

            Mob.SetDestination(newPosition);

        }
    }
    
    
}
