using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMenu : MonoBehaviour
{
    //When the trigger happens
    void OnTriggerEnter(Collider other)
    {

        Debug.Log ("End fo Tutorial");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        
    }
}
