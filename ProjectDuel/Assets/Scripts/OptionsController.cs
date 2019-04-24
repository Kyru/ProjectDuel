using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource[] audioSources;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
    }

    public void onVolumeChange(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        foreach(AudioSource audio in audioSources)
        {
            audio.volume = volume;
        }
    }
}
