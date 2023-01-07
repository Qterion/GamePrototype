using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStats : EnemyHealth
{

    [SerializeField] private float damage = 10f;
    [SerializeField] public float attackSpeed = 1.5f;
    [SerializeField] private bool canAttack = true;
    private playerHealth health;

    GameObject target;


    // Start is called before the first frame update

    public void DealDamage()
    {
        target.GetComponent<playerHealth>().TakeDamage(damage);
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("ZombieSpeed"))
        {
            this.GetComponent<NavMeshAgent>().speed = PlayerPrefs.GetFloat("ZombieSpeed");

        }
        if (PlayerPrefs.HasKey("ZombieDamage"))
        {
            damage = PlayerPrefs.GetFloat("ZombieDamage");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
