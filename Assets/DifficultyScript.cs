using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Methods that changes the player prefs based on the difficulty chosen by the player
    public void EasyDifficulty()
    {
        
        PlayerPrefs.SetFloat("ZombieSpeed", 2f);
        PlayerPrefs.SetFloat("ZombieDamage",5f);
        PlayerPrefs.SetFloat("BulletDamage", 1.5f);
        PlayerPrefs.SetFloat("PlayerHP", 200f);
        ResetSettings();

    }
    public void MediumDifficulty()
    {
        
        PlayerPrefs.SetFloat("ZombieSpeed", 3.5f);
        PlayerPrefs.SetFloat("ZombieDamage", 10f);
        PlayerPrefs.SetFloat("BulletDamage", 1f);
        PlayerPrefs.SetFloat("PlayerHP", 100f);
        ResetSettings();

    }
    public void HardDifficulty()
    {
        
        PlayerPrefs.SetFloat("ZombieSpeed", 4.5f);
        PlayerPrefs.SetFloat("ZombieDamage", 15f);
        PlayerPrefs.SetFloat("BulletDamage", 0.75f);
        PlayerPrefs.SetFloat("PlayerHP", 75f);
        ResetSettings();

    }

    //method that deletes not needed player prefs before start of the campaign
    public void ResetSettings()
    {
        PlayerPrefs.DeleteKey("JumpForce");
        PlayerPrefs.DeleteKey("SprintSpeed");
        PlayerPrefs.DeleteKey("TakeFallDamage");
    }
}
