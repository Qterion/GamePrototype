using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        GameObject[] enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyLeft = enemyCount.Length;
        if (enemyLeft == 0) {
            SceneManager.LoadScene(1);
        }
    }
}
