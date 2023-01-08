using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SetVolume : MonoBehaviour {
    public AudioMixer audioMixer;
    public AudioMixer SFXMixer;
    public Slider AudioSlider;
    public Slider GameVolSlider;
    public float Audiovol;
    public float GameVol;


    public void Start()
    {
        if (PlayerPrefs.HasKey("AudioVol"))
        {
            Audiovol = PlayerPrefs.GetFloat("AudioVol");
            if (AudioSlider != null)
            {
                AudioSlider.value = Audiovol;
            }
            BGMAdjustment();


        }
        if (PlayerPrefs.HasKey("GameVol"))
        {
            GameVol = PlayerPrefs.GetFloat("GameVol");
            if (GameVolSlider != null)
            {
                GameVolSlider.value = GameVol;
            }
            SFXAdjustment();


        }

    }
    public void Update()
    {
        PlayerPrefs.SetFloat("AudioVol", Audiovol);
        PlayerPrefs.SetFloat("GameVol", GameVol);
    }
    public void SetBGMVolumeLevel (float currentBGMSliderVal)
    {
        Audiovol = currentBGMSliderVal;
        BGMAdjustment();
        
    }
    public void SetGameVolume(float currentBGMSliderVal)
    {
        GameVol = currentBGMSliderVal;
        SFXAdjustment();
    }
    public void BGMAdjustment()
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(Audiovol) * 20);
        }

    }
    public void SFXAdjustment()
    {
        SFXMixer.SetFloat("GameVol", Mathf.Log10(GameVol) * 20);
    }
}