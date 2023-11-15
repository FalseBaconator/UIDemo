using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgMusic;
    public AudioSource SFX;

    public Slider bgSlider;
    public Slider SFXSlider;

    private void Start()
    {
        bgMusic.volume = PlayerPrefs.GetFloat("BGVolume", 1);
        SFX.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
        bgSlider.value = bgMusic.volume;
        SFXSlider.value = SFX.volume;
    }

    public void ChangeBGVolume(Slider slider)
    {
        bgMusic.volume = slider.value;
        PlayerPrefs.SetFloat("BGVolume", slider.value);
    }

    public void ChangeSFXVolume(Slider slider)
    {
        SFX.volume = slider.value;
        PlayerPrefs.SetFloat("SFXVolume", slider.value);
    }

    public void PlayJump()
    {
        SFX.Play();
    }

}
