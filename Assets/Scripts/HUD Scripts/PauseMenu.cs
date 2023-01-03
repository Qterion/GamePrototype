using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //initialises pause menu property variables
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //Allows 'escape' button to trigger pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Checks if the game is pause or not
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //This method makes the game playabable back when it resumes
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //This method stops the game when it is paused
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadSettings()
    {
        Debug.Log ("Load Settings...");
        SceneManager.LoadScene("Settings");
    }

    public void LoadCredits()
    {
        Debug.Log ("Loading Credits...");
    }

    public void QuitGame()
    {
        Debug.Log ("Quitting game...");
    }
}





