using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakItController : MonoBehaviour
{

    public float JumpDefault = 10f;
    public float SprintDefault = 10f;
    public float HealthDefault = 100f;
    public float ZombieSpeedDefault = 3.5f;
    public float ZombieDamageDefault = 10f;
    public float BulletDamageDefault = 1f;
    public int FallDamageDefault = 1;
    public Slider JumpSlider;
    public Slider SprintSlider;
    public Slider HealthSlider;
    public Slider ZombieSpeedSlider;
    public Slider ZombieDamageSlider;
    public Toggle FallDamageToggle;
    public Slider BulletDamageSlider;
    public float JumpHeight;
    public float SprintSpeed;
    public float HealthAmount;
    public float ZombieSpeed;
    public float ZombieDamage;
    public int FallDamage;
    public float BulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("JumpForce"))
        {
            JumpHeight = PlayerPrefs.GetFloat("JumpForce");
        }
        else
        {
            JumpHeight = JumpDefault;
        }
        if (PlayerPrefs.HasKey("SprintSpeed"))
        {
            SprintSpeed = PlayerPrefs.GetFloat("SprintSpeed");
        }
        else
        {
            SprintSpeed = SprintDefault;
        }
        if (PlayerPrefs.HasKey("PlayerHP"))
        {
            HealthAmount = PlayerPrefs.GetFloat("PlayerHP");
        }
        else
        {
            HealthAmount = HealthDefault;
        }
        if (PlayerPrefs.HasKey("ZombieSpeed"))
        {
            ZombieSpeed = PlayerPrefs.GetFloat("ZombieSpeed");
        }
        else
        {
            ZombieSpeed = ZombieSpeedDefault;
        }
        if (PlayerPrefs.HasKey("ZombieDamage"))
        {
            ZombieDamage = PlayerPrefs.GetFloat("ZombieDamage");
        }
        else
        {
            ZombieDamage = ZombieDamageDefault;
        }
        if (PlayerPrefs.HasKey("TakeFallDamage"))
        {
            FallDamage =PlayerPrefs.GetInt("TakeFallDamage");
        }
        else
        {
            FallDamage = FallDamageDefault;
        }
        if (PlayerPrefs.HasKey("BulletDamage"))
        {
            BulletDamage = PlayerPrefs.GetFloat("BulletDamage");
        }
        else
        {
            BulletDamage = BulletDamageDefault;
        }

        

        JumpSlider.value = JumpHeight;
        SprintSlider.value = SprintSpeed;
        HealthSlider.value = HealthAmount;
        ZombieSpeedSlider.value = ZombieSpeed;
        ZombieDamageSlider.value = ZombieDamage;
        BulletDamageSlider.value = BulletDamage;
        if (FallDamage == 0)
        {
            FallDamageToggle.isOn = false;
        }
        else
        {
            FallDamageToggle.isOn = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("JumpForce", JumpHeight);
        PlayerPrefs.SetFloat("SprintSpeed", SprintSpeed);
        PlayerPrefs.SetFloat("PlayerHP", HealthAmount);
        PlayerPrefs.SetFloat("ZombieSpeed", ZombieSpeed);
        PlayerPrefs.SetFloat("ZombieDamage", ZombieDamage);
        PlayerPrefs.SetInt("TakeFallDamage", FallDamage);
        PlayerPrefs.SetFloat("BulletDamage", BulletDamage);

    }
    public void ChangeJumpForce(float value)
    {
        JumpHeight = value;
    }
    public void ChangeSprintSpeed(float value)
    {
        SprintSpeed = value;
    }
    public void ChangeHealth(float value)
    {
        HealthAmount = value;
    }
    public void ChangeZombieSpeed(float value)
    {
        ZombieSpeed = value;
    }
    public void ChangeZombieDamage(float value)
    {
        ZombieDamage = value;
    }
    public void ChangeBulletDamage(float value)
    {
        BulletDamage = value;
    }
    public void ChangeFallDamage(bool value)
    {
        
        if (value==true)
        {
            FallDamage = 1;
        }
        else
        {
            FallDamage = 0;
        }
        Debug.Log(FallDamage);
        
    }
}
