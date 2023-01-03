using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayStory() {
        SceneManager.LoadScene(1);
    }

    public void PlaySurvival() {
        SceneManager.LoadScene(4);
    }

    public void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
