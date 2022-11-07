using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float respawnTime = 5f;
    private float nextSpawnTime;
    [SerializeField]
    private int maxEnemy;


  

    // Update is called once per frame
    private void Update()
    {
        GameObject[] enemyCount = GameObject.FindGameObjectsWithTag("Enemy");

        int thingyCount = enemyCount.Length;
        Debug.Log(thingyCount);
        



        if (thingyCount < maxEnemy){
            if (Time.time >= nextSpawnTime)
            {
                nextSpawnTime = Time.time + respawnTime;

                GameObject.Instantiate(enemyPrefab, transform.position, transform.rotation);
            }
        }



    }
       
}
