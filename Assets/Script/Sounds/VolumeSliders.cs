using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class VolumeSliders : MonoBehaviour
{
    public bool volumeType;

    private Slider volumeSlider;

    private void Awake() 
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }

    private void Update() 
    {
        switch (volumeType)
        {
            case false:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case true:
                volumeSlider.value = AudioManager.instance.sfxVolume;
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case false:
                AudioManager.instance.musicVolume = volumeSlider.value;
                break;
            case true:
                AudioManager.instance.sfxVolume = volumeSlider.value;
                break;
        }
    }
}
