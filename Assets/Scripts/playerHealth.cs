using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public static float PlayerMaxHP = 100f;
    public static float PlayerHP = 100f;
    public TextMeshProUGUI PlayerHPText;
    public static bool GameOver;
    void Start()
    {
        GameOver = false;
        PlayerHP = PlayerMaxHP;
    }
    // Update is called once per frame
    void Update()
    {
        //Changes player health bar value on ui
        PlayerHPText.text = "+" + PlayerHP;

        // if gameover is true it restarts the current scene
        if (GameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // basic method that takes as a paraeter a float value and substracts it from current health
    public void TakeFallDamage(float damage)
    {
        PlayerHP = PlayerHP - damage;
        CheckHealth();
    }

    //if the player health is 0 sets gameover to true
    private void CheckHealth()
    {
        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            GameOver = true;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (PlayerHP - damageAmount <= 0)
        {
            PlayerHP = 0;
            GameOver = true;
        }
        else
        {
            PlayerHP -= damageAmount;
        }
    }
}
