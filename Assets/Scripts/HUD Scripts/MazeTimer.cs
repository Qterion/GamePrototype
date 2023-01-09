using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeTimer : MonoBehaviour
{
    public float mazeTimer = 300;
    public Text timerDisplayText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Reduces timer each frame
        if (mazeTimer > 0) {
            mazeTimer -= Time.deltaTime;
        }
        //Sets timer to 0 if it goes below the 0 mark
        else {
            mazeTimer = 0;
            Debug.Log ("Loading LosingEndScene...");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene(12);
        }

        SetTimerDisplayText(mazeTimer);
    }

    //Updates the timer text with the current remaining time
    void SetTimerDisplayText(float remainingTime)
    {
        //Sets display timer to 0 if it goes below the 0 mark
        if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        //Changes float to int
        int minutes = (int)remainingTime / 60;
        int seconds = (int)remainingTime % 60;

        string strMinutes = "00";
        string strSeconds = "00";

        //formats int minutes to string in clock style format
        if (minutes < 10) {
            strMinutes = "0" + minutes.ToString();
        }
        else {
            strMinutes = minutes.ToString();
        }

        //formats int seconds to string in clock style format
        if (seconds < 10)
        {
            strSeconds = "0" + seconds.ToString();
        }
        else
        {
            strSeconds = seconds.ToString();
        }

        //Changes text to show updated minutes and seconds
        timerDisplayText.text = strMinutes + ":" + strSeconds;

    }
}
