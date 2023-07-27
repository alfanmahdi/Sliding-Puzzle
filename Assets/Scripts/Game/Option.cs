using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public AudioMixer audioMixerMusic;
    public AudioMixer audioMixerSFX;
    private float value;
    public Toggle fullScreen;

    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        audioMixerMusic.GetFloat("volume", out float music);
        audioMixerSFX.GetFloat("volume", out float effect);

        if (music == -80)
        {
            musicSlider.value = -30;
        }
        else musicSlider.value = music;

        if (effect == -80)
        {
            sfxSlider.value = -30;
        }
        else sfxSlider.value = effect;
    }

    private void Update()
    {
        fullScreen.isOn = Screen.fullScreen;
    }
    public void setVolume(float volume)
    {
        audioMixerMusic.SetFloat("volume", volume);
        if(musicSlider.value == -30)
        {
            audioMixerMusic.SetFloat("volume", -80);
        }
    }

    public void setSFX(float volume)
    {
        audioMixerSFX.SetFloat("volume", volume);
        if (sfxSlider.value == -30)
        {
            audioMixerSFX.SetFloat("volume", -80);
        }
    }

    public void setFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}