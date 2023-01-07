using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public float VerticalSenseDefault = 3f;
    public float HorizontalSenseDefault = 300f;
    public Slider verticalSlider;
    public Slider horizontalSlider;
    public float Vsense;
    public float Hsense;

    void Start()
    {

        
        if (PlayerPrefs.HasKey("Vsense"))
        {
            Vsense = PlayerPrefs.GetFloat("Vsense");
        }
        else
        {
            Vsense = VerticalSenseDefault;
        }
        if (PlayerPrefs.HasKey("Hsense"))
        {
            Hsense = PlayerPrefs.GetFloat("Hsense");
        }
        else
        {
            Hsense = HorizontalSenseDefault;
        }
        
        verticalSlider.value = Vsense;
        horizontalSlider.value = Hsense;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("Hsense", Hsense);
        PlayerPrefs.SetFloat("Vsense", Vsense);
    }
    public void VerticalSense(float newVal)
    {
        Vsense = newVal;

    }
    public void HorizontalSense(float newVal)
    {
        Hsense = newVal;
    }
}
