using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener((float value) => SetVolume(value));
    }


    public void SetVolume(float volume)
    {
        if (volume <= -40)
            volume = -80;

        mixer.SetFloat("MasterVolume", volume);
    }
}
