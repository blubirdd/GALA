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
                audioSource.PlayOneShot(sounds[0], 0.5f);
                break;
            case 1:
                audioSource.PlayOneShot(sounds[1], 1f);
                break;
            case 2:
                audioSource.PlayOneShot(sounds[2], 1f);
                break;
            case 3:
                audioSource.PlayOneShot(sounds[3], 0.5f);
                break;
            case 4:
                audioSource.PlayOneShot(sounds[4], 0.8f);
                break;
            case 5:
                audioSource.PlayOneShot(sounds[5], 0.7f);
                break;
            case 6:
                audioSource.PlayOneShot(sounds[6], 1f);
                break;
            case 7:
                audioSource.PlayOneShot(sounds[7], 1f);
                break;
            case 8:
                audioSource.PlayOneShot(sounds[8], 1f);
                break;
            case 9:
                audioSource.PlayOneShot(sounds[9], 0.5f);
                break;
            case 10:
                audioSource.PlayOneShot(sounds[10], 1f);
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
