using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : EnemyHealth
{

    [SerializeField] private int damage = 10;
    [SerializeField] private float attackSpeed = 1.5f;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
