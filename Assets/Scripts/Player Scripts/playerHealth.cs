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
    public GameObject HealthbarHealth;

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
            HealthSlider.maxValue = PlayerMaxHP;
            HealthSlider.value = PlayerHP;
            //HealthbarHealthColour = HealthbarHealth.GetComponent<Image>().color;
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("PlayerHP", PlayerMaxHP);
        //Changes player health bar value on ui
        if (PlayerHPText != null)
        {
            PlayerHPText.text = PlayerHP.ToString();
        }

        if (HealthSlider != null)
        {
            HealthSlider.value = PlayerHP;
            if (PlayerHP > (PlayerMaxHP/2))
            {
                HealthbarHealth.GetComponent<Image>().color = new Color32(60, 253, 18, 255);
            }
            else if (PlayerHP > (PlayerMaxHP/4) & PlayerHP <= (PlayerMaxHP/2))
            {
                HealthbarHealth.GetComponent<Image>().color = new Color32(255, 172, 20, 255);
            }
            else {
                HealthbarHealth.GetComponent<Image>().color = new Color32(255, 20, 22, 255);
            }
        }



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
    public void TakeDamage(float damageAmount)
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
