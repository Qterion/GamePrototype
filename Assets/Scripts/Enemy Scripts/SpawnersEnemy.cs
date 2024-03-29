using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] spawners;


    [SerializeField]
    private GameObject enemy;


    [SerializeField]
    private float respawnTime;  // = 5f
    private float nextSpawnTime;
    [SerializeField]
    private int maxEnemyInScene;
    [SerializeField]
    private int points;
    [SerializeField]
    private int enemiesToSpawn;
    private int enemiesSpawned = 0;


    private void Start()
    {
        spawners = new GameObject[points];

        for (int i = 0; i < spawners.Length; i++) {
            spawners[i] = transform.GetChild(i).gameObject;

         }
    }

    private void spawnEnemy()

    {

        if (enemiesSpawned <= enemiesToSpawn)
        {
            int spawnerID = Random.Range(0, spawners.Length);
            GameObject.Instantiate(enemy, spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);
            enemiesSpawned++;



        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            spawnEnemy();
        }
    }*/



    private void Update()
    {
        GameObject[] enemyCount = GameObject.FindGameObjectsWithTag("Enemy");

        int thingyCount = enemyCount.Length;
        //Debug.Log(thingyCount);


      
        if (thingyCount < maxEnemyInScene)
        {
            if (Time.time >= nextSpawnTime)
            {
                nextSpawnTime = Time.time + respawnTime;

                spawnEnemy();

                    
            }


        }
       
    }







    }
