using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume", 0.5f);
        foreach(AudioSource audio in audioSources)
        {
            audio.volume = volume;
        }
    }
}
