using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region Singleton

    public static SoundManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one SoundManager of task found");
            return;
        }

        instance = this;
    }
    #endregion

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    public AudioClip[] sounds;

    public void PlaySoundFromClips(int sound)
    {
        switch (sound)
        {
            case 0:
                audioSource.PlayOneShot(sounds[0], 0.4f);
                break;
            default:
                Debug.LogError("No sound sound");
                break;
        }
    }
    public void PlaySound(AudioClip clip)
    { 
        audioSource.PlayOneShot(clip);  
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
