using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour

    
{
    //[SerializeField] private float stoppingDistance = 3;
    float timeOfLastAttack = 0;
    private NavMeshAgent agent = null;
    private bool hasStooped = false;

    private Animator anim = null;
    private ZombieStats stats = null;

    //[SerializeField] private Transform target;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        MoveToTarget();
    }

    private void MoveToTarget ()
    {
        agent.SetDestination(target.transform.position);
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget <= agent.stoppingDistance)
        {
            anim.SetFloat("Speed", 0f);
            // aatck target
            if(!hasStooped)
            {
                 hasStooped = true;
                 timeOfLastAttack = Time.time;
            }
           


            if(Time.time >= timeOfLastAttack + stats.attackSpeed )
            {

                timeOfLastAttack = Time.time;
              
                AttackTarget();   

            }
           


        }
        else
        {
            if(hasStooped)
            {
                hasStooped = false;
            }
        }

    }


    private void AttackTarget()
    {
        anim.SetTrigger("attack");
        stats.DealDamage();
    }

    private void RotateToTarget ()
    {
        transform.LookAt(target.transform.position);

        //Vector3 direction = target.transform.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        //transform.rotation = rotation;

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

    private void LateUpdate()
    {
        //Fixes the zombie X and Y rotation to stop them from rotating/falling over when colliding with other objects
        transform.localEulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }



}
