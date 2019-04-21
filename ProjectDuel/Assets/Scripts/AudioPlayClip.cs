using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayClip : MonoBehaviour
{
    public AudioClip[] audioClips;
    public int clipIndex;
    [SerializeField] private AudioSource audioSourceMain;
    [SerializeField] private AudioSource audioSourceSoundFX;

    private void Start()
    {
        PlayAudioClip(clipIndex);
    }

    public void PlayAudioClip(int clipToPlay)
    {
        if (clipToPlay == 0)
        {
            audioSourceMain.clip = audioClips[clipToPlay];
            audioSourceMain.Play();
        }
        else
        {
            audioSourceSoundFX.clip = audioClips[clipToPlay];
            audioSourceSoundFX.Play();
        }
    }
}