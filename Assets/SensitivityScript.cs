using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class SensitivityScript : MonoBehaviour
{
    public CinemachineFreeLook NormalView;
    public CinemachineVirtualCamera CombatView;
    public Slider verticalSlider;
    public Slider horizontalSlider;
    private float Vsense = 2f;
    private float Hsense = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
        Vsense = PlayerPrefs.GetFloat("Vsense");
        Hsense = PlayerPrefs.GetFloat("Hsense");
        NormalView.m_XAxis.m_MaxSpeed = Hsense;
        NormalView.m_YAxis.m_MaxSpeed = Vsense;
        verticalSlider.value = Vsense;
        horizontalSlider.value = Hsense;
    }

    // Update is called once per frame
    void Update()
    {
        NormalView.m_XAxis.m_MaxSpeed = Hsense;
        NormalView.m_YAxis.m_MaxSpeed = Vsense;
        PlayerPrefs.SetFloat("Hsense", Hsense);
        PlayerPrefs.SetFloat("Vsense", Vsense);
        
    }
    public void VerticalSense(float newVal)
    {
        Vsense = newVal;
        Debug.Log(Vsense);
        Debug.Log(newVal);
    }
    public void HorizontalSense(float newVal)
    {
        Hsense = newVal;
    }
}
