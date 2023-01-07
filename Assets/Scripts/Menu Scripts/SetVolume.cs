using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SetVolume : MonoBehaviour {
    public AudioMixer audioMixer;
    public Slider AudioSlider;
    public float Audiovol;



    public void Start()
    {
        if (PlayerPrefs.HasKey("AudioVol"))
        {
            Audiovol = PlayerPrefs.GetFloat("AudioVol");
            if (AudioSlider != null)
            {
                AudioSlider.value = Audiovol;
            }
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(Audiovol) * 20);

        }

    }
    public void Update()
    {
        PlayerPrefs.SetFloat("AudioVol", Audiovol);
    }
    public void SetBGMVolumeLevel (float currentBGMSliderVal)
    {
        Audiovol = currentBGMSliderVal;
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(Audiovol) * 20);
    }
}