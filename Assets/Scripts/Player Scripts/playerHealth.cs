using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class playerHealth : MonoBehaviour
{
    //initialises player health property variables
    public float PlayerMaxHP = 100f;
    public float PlayerHP = 100f;
    public TextMeshProUGUI PlayerHPText;
    public static bool GameOver;
    public Slider HealthSlider;
    // Start is called before the first frame update

    public void PlayerHealthSet(float value)
    {
        PlayerMaxHP = value;
        PlayerHP = value;
    }
    void Start()
    {
        GameOver = false;
        
        if (PlayerPrefs.HasKey("PlayerHP"))
        {
            PlayerMaxHP = PlayerPrefs.GetFloat("PlayerHP");

        }
        PlayerHP = PlayerMaxHP;
        if (HealthSlider !=null){
            HealthSlider.value = PlayerMaxHP;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("PlayerHP", PlayerMaxHP);
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

    // if the player health is 0 sets gameover to true
    private void CheckHealth()
    {
        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            GameOver = true;
        }
    }

    // Reduces player health by the damage amount, if player health is less than 0 then ends the game
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
