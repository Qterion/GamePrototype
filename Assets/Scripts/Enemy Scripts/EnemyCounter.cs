using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    public Text enemyCountText;
    

    // Update is called once per frame
    public void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemyCountText.text = "Enemies: " + enemies.Length.ToString();
    }
}
