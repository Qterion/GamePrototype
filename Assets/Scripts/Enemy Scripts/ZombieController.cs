using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour

    
{
    [SerializeField] private float stoppingDistance = 3;
    float lastAttackTime = 0;
    float attackCooldown = 5;
    private NavMeshAgent agent = null;
    private Animator anim = null;
    private ZombieStats stats = null;

    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {

        MoveToTarget();
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < stoppingDistance)
        {
            StopEnemy();
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                MoveToTarget();
            }

        }
        else
        {
            MoveToTarget();
        }

    }

    private void MoveToTarget ()
    {
        agent.SetDestination(target.position);
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= agent.stoppingDistance)
        {
            anim.SetFloat("Speed", 0f);
            // aatck target
            AttackTarget();   


        }

    }


    private void AttackTarget()
    {
        anim.SetTrigger("attack");
        stats.DealDamage();
    }

    private void RotateToTarget ()
    {
        //transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;

    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<ZombieStats>();


    }

    private void StopEnemy()
    {
        agent.isStopped = true;

    }



}
