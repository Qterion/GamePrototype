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
        //if (enemyLeft == 0) {
            Debug.Log ("Loading Narration2...");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene(7);
        //}
    }

    public void LoadMainMenu(){
        Debug.Log ("Loading Main Menu...");
        SceneManager.LoadScene(0);
    }

    public void LoadNarrationScene()
    {
        Debug.Log ("Loading Narration Scene...");
        SceneManager.LoadScene(5);
    }

    public void LoadStartScene()
    {
        Debug.Log ("Loading Start Scene...");
        SceneManager.LoadScene(6);
    }

    public void LoadLevel2()
    {
        Debug.Log ("Loading Level 2...");
        SceneManager.LoadScene(2);
    }

    public void LoadLevel3()
    {
        Debug.Log ("Loading Level 3...");
        SceneManager.LoadScene(9);
    }
}
