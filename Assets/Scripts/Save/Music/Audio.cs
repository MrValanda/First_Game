using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [FormerlySerializedAs("_musicToggle")] [SerializeField] private Slider _musicSlider;
    [FormerlySerializedAs("_soundToggle")] [SerializeField] private Slider _soundSlider;
    [FormerlySerializedAs("_masterToggle")] [SerializeField] private Slider _masterSlider;

    private void Start()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        _soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

       
        MasterChangeVolume(_masterSlider.value);
        SoundChangeVolume(_soundSlider.value);
        MusicChangeVolume(_musicSlider.value);
    }

    public void MasterChangeVolume(float volume)
    {
        _mixer.audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume",volume);
        PlayerPrefs.Save();
    }
    public void MusicChangeVolume(float volume)
    {
        _mixer.audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume",volume);
        PlayerPrefs.Save();

        
    }
    public void SoundChangeVolume(float volume)
    {
        _mixer.audioMixer.SetFloat("SoundVolume", volume);
        PlayerPrefs.SetFloat("SoundVolume",volume);
        PlayerPrefs.Save();

    }
}
