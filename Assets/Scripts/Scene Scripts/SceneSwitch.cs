using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    //When the trigger happens
    void OnTriggerEnter(Collider other)
    {
        //check how many enemies are there
        GameObject[] enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyLeft = enemyCount.Length;

        //if it no enemies are left load the level 2
        if (enemyLeft == 0) {
            SceneManager.LoadScene(1);
        }
    }
}
