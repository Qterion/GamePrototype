using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public static int PlayerHP = 100;
    public TextMeshProUGUI PlayerHPText;
    public static bool GameOver;
    void Start()
    {
        GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHPText.text = "<3" + PlayerHP;
        if (GameOver)
        {

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
