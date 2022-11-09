using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float stoppingDistance;
    [SerializeField] int damage;
    float lastAttackTime = 0;
    float attackCooldown = 5;

    NavMeshAgent agent;
    GameObject target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    private void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < stoppingDistance)
        {
            StopEnemy();
            if(Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                target.GetComponent<playerHealth>().TakeDamage(damage);
            }
            
        }
        else
        {
            GoToTarget();
        }
    }


    private void GoToTarget ()
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    private void StopEnemy()
    {
        agent.isStopped = true;

    }
}
